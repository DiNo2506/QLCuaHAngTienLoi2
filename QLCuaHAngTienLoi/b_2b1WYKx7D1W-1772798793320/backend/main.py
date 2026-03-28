from flask import Flask, jsonify, request
from flask_cors import CORS
from datetime import datetime
import json

app = Flask(__name__)
CORS(app)

# In-memory database for demo
products_db = [
    {
        "id": 1,
        "name": "Mì gói",
        "company": "CÔNG TY BA",
        "sku": "SP001",
        "category": "Thực phẩm",
        "price": 5000,
        "stock": 50,
        "created_at": "2026-03-01"
    },
    {
        "id": 2,
        "name": "snack tôm",
        "company": "CÔNG TY BA",
        "sku": "SP002",
        "category": "Bánh keo",
        "price": 5000,
        "stock": 50,
        "created_at": "2026-03-02"
    },
    {
        "id": 3,
        "name": "gao",
        "company": "công ty gao",
        "sku": "SP003",
        "category": "Thực phẩm",
        "price": 30000,
        "stock": 20,
        "created_at": "2026-03-03"
    }
]

companies_db = [
    {"id": 1, "name": "CÔNG TY BA", "products": 2, "value": 200200},
    {"id": 2, "name": "công ty gao", "products": 1, "value": 500000}
]

# Dashboard endpoints
@app.get("/api/health")
def health():
    return jsonify({"status": "ok"})

@app.get("/api/dashboard")
def get_dashboard():
    total_products = len(products_db)
    total_revenue = sum(p["price"] * p["stock"] for p in products_db)
    total_alerts = sum(1 for p in products_db if p["stock"] < 10)
    total_favorites = 2
    
    return jsonify({
        "total_products": total_products,
        "total_revenue": total_revenue,
        "total_alerts": total_alerts,
        "total_favorites": total_favorites,
        "date": datetime.now().strftime("%d/%m/%Y")
    })

# Products endpoints
@app.get("/api/products")
def get_products():
    search = request.args.get("search", "").lower()
    sort_by = request.args.get("sort", "all")
    
    filtered = products_db
    
    if search:
        filtered = [p for p in filtered if search in p["name"].lower() or search in p["company"].lower()]
    
    if sort_by == "price_asc":
        filtered = sorted(filtered, key=lambda x: x["price"])
    elif sort_by == "price_desc":
        filtered = sorted(filtered, key=lambda x: x["price"], reverse=True)
    elif sort_by == "stock_asc":
        filtered = sorted(filtered, key=lambda x: x["stock"])
    elif sort_by == "stock_desc":
        filtered = sorted(filtered, key=lambda x: x["stock"], reverse=True)
    
    return jsonify(filtered)

@app.post("/api/products")
def create_product():
    data = request.json
    new_product = {
        "id": max([p["id"] for p in products_db]) + 1 if products_db else 1,
        "name": data.get("name"),
        "company": data.get("company"),
        "sku": data.get("sku"),
        "category": data.get("category"),
        "price": int(data.get("price", 0)),
        "stock": int(data.get("stock", 0)),
        "created_at": datetime.now().strftime("%Y-%m-%d")
    }
    products_db.append(new_product)
    return jsonify(new_product), 201

@app.put("/api/products/<int:product_id>")
def update_product(product_id):
    product = next((p for p in products_db if p["id"] == product_id), None)
    if not product:
        return jsonify({"error": "Product not found"}), 404
    
    data = request.json
    product.update({
        "name": data.get("name", product["name"]),
        "company": data.get("company", product["company"]),
        "sku": data.get("sku", product["sku"]),
        "category": data.get("category", product["category"]),
        "price": int(data.get("price", product["price"])),
        "stock": int(data.get("stock", product["stock"]))
    })
    return jsonify(product)

@app.delete("/api/products/<int:product_id>")
def delete_product(product_id):
    global products_db
    products_db = [p for p in products_db if p["id"] != product_id]
    return jsonify({"success": True})

# Reports endpoints
@app.get("/api/reports/top-products")
def get_top_products():
    sorted_products = sorted(products_db, key=lambda x: x["stock"], reverse=True)
    return jsonify([{
        "rank": i + 1,
        "name": p["name"],
        "stock": p["stock"]
    } for i, p in enumerate(sorted_products[:3])])

@app.get("/api/reports/categories")
def get_category_distribution():
    categories = {}
    for product in products_db:
        cat = product["category"]
        if cat not in categories:
            categories[cat] = 0
        categories[cat] += 1
    
    return jsonify({
        "categories": [{"name": k, "count": v} for k, v in categories.items()]
    })

@app.get("/api/reports/companies")
def get_companies():
    return jsonify(companies_db)

# Warehouse endpoints
@app.get("/api/warehouse")
def get_warehouse():
    return jsonify({
        "total_products": len(products_db),
        "low_stock_alerts": sum(1 for p in products_db if p["stock"] < 10),
        "companies": companies_db
    })

if __name__ == "__main__":
    app.run(debug=False)
