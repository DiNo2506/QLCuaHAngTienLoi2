// Global JavaScript utilities for the store management system

// API Base URL - automatically uses /api prefix for all requests
const API_BASE = '/api';

// Utility function to format currency
function formatCurrency(amount) {
    return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND'
    }).format(amount);
}

// Utility function to format date
function formatDate(dateString) {
    const date = new Date(dateString);
    return date.toLocaleDateString('vi-VN');
}

// Fetch with error handling
async function fetchAPI(endpoint, options = {}) {
    try {
        const response = await fetch(`${API_BASE}${endpoint}`, {
            headers: {
                'Content-Type': 'application/json',
                ...options.headers
            },
            ...options
        });

        if (!response.ok) {
            throw new Error(`API Error: ${response.status}`);
        }

        return await response.json();
    } catch (error) {
        console.error('API Error:', error);
        throw error;
    }
}

// Initialize tooltips and other UI enhancements
document.addEventListener('DOMContentLoaded', () => {
    // Add any global UI initialization here
    console.log('Store Management System Loaded');
});
<script>
    function loadCart(){
    return JSON.parse(localStorage.getItem("cart") || "[]");
}

    function saveCart(cart){
        localStorage.setItem("cart", JSON.stringify(cart));
}

    function renderCart(){
    const cart = loadCart();
    const el = document.getElementById("cart-items");
    const totalEl = document.getElementById("cart-total");

    if(!el) return;

    let html = "";
    let total = 0;

    cart.forEach((item,i)=>{
        total += item.price * item.qty;

    html += `
    <div class="cart-item">
        <span>${item.name} x${item.qty}</span>
        <span>${format(item.price * item.qty)}</span>
    </div>`;
    });

    el.innerHTML = html;
    totalEl.innerText = format(total);
}

    function addToCart(id,name,price){
        let cart = loadCart();
    let item = cart.find(x=>x.id==id);

    if(item) item.qty++;
    else cart.push({id, name, price, qty:1});

    saveCart(cart);
    renderCart();
}

    function format(n){
    return n.toLocaleString('vi-VN') + "?";
}

    function checkout(){
        alert("Thanh toán thành công");
    localStorage.removeItem("cart");
    renderCart();
}

    document.addEventListener("DOMContentLoaded",renderCart);
</script>