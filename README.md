🛒 QLCuaHAngTienLoi
Hệ thống Quản Lý Cửa Hàng Tiện Lợi được xây dựng bằng ASP.NET / C# nhằm hỗ trợ quản lý sản phẩm, tồn kho, nhà cung cấp và bán hàng.

📌 Giới thiệu
QLCuaHAngTienLoi là một ứng dụng web giúp quản lý hoạt động của cửa hàng tiện lợi bao gồm:

Quản lý sản phẩm
Quản lý danh mục
Quản lý nhà cung cấp
Quản lý tồn kho
Giỏ hàng
Bán hàng
Dự án được phát triển phục vụ học tập và nghiên cứu lập trình Web với ASP.NET.

🚀 Tính năng
📦 Quản lý sản phẩm
Thêm sản phẩm
Sửa thông tin sản phẩm
Xóa sản phẩm
Tìm kiếm sản phẩm
Hiển thị danh sách sản phẩm
🗂 Quản lý danh mục
Thêm danh mục
Sửa danh mục
Xóa danh mục
🚚 Quản lý nhà cung cấp
Thêm nhà cung cấp
Sửa thông tin nhà cung cấp
Xóa nhà cung cấp
📊 Quản lý tồn kho
Theo dõi số lượng sản phẩm
Cập nhật khi nhập hàng
Cập nhật khi bán hàng
🛒 Giỏ hàng
Thêm sản phẩm vào giỏ
Cập nhật số lượng
Xóa sản phẩm khỏi giỏ
Lưu dữ liệu bằng LocalStorage
🧰 Công nghệ sử dụng
Backend
ASP.NET
C#
Entity Framework
LINQ
Frontend
HTML
CSS
JavaScript
Bootstrap
Database
SQL Server
Tools
Visual Studio
Git
GitHub
📂 Cấu trúc dự án Dự án được tổ chức theo mô hình ASP.NET MVC, giúp tách biệt rõ ràng giữa logic xử lý, dữ liệu và giao diện.

QLCuaHAngTienLoi │ ├── Controllers │ ├── SanPhamController.cs │ ├── DanhMucController.cs │ ├── DonHangController.cs │ └── HomeController.cs │ ├── Models │ ├── SanPham.cs │ ├── DanhMuc.cs │ ├── NhaCungCap.cs │ └── DonHang.cs │ ├── Data │ └── QLCuaHangContext.cs │ ├── Views │ ├── SanPham │ │ ├── Index.cshtml │ │ ├── Create.cshtml │ │ ├── Edit.cshtml │ │ └── Delete.cshtml │ │ │ ├── DanhMuc │ ├── DonHang │ └── Shared │ ├── wwwroot │ ├── css │ ├── js │ ├── images │ └── lib │ ├── appsettings.json ├── Program.cs ├── Startup.cs └── README.md Giải thích các thư mục 📁 Controllers

Chứa các controller xử lý logic của hệ thống và nhận request từ người dùng.

Ví dụ:

SanPhamController → quản lý sản phẩm DanhMucController → quản lý danh mục DonHangController → xử lý đơn hàng 📁 Models

Chứa các class mô hình dữ liệu tương ứng với các bảng trong database.

Ví dụ:

SanPham → thông tin sản phẩm DanhMuc → danh mục sản phẩm NhaCungCap → nhà cung cấp 📁 Data

Chứa DbContext dùng để kết nối và thao tác với cơ sở dữ liệu thông qua Entity Framework.

📁 Views

Chứa các file .cshtml để hiển thị giao diện người dùng.

Các thư mục con tương ứng với từng controller.

Ví dụ:

Views/SanPham 📁 wwwroot

Chứa các tài nguyên tĩnh của website:

CSS JavaScript Hình ảnh Thư viện frontend ⚙️ Cài đặt và chạy dự án (Installation) 1️⃣ Clone repository git clone https://github.com/DiNo2506/QLCuaHAngTienLoi.git

Sau đó di chuyển vào thư mục dự án:

cd QLCuaHAngTienLoi 2️⃣ Mở project

Mở project bằng Visual Studio

Chọn:

Open a project or solution

Sau đó mở file:

QLCuaHAngTienLoi.sln 3️⃣ Cấu hình cơ sở dữ liệu

Mở file:

appsettings.json

Cập nhật Connection String:

"ConnectionStrings": { "DefaultConnection": "Server=.;Database=QLCuaHangTienLoi;Trusted_Connection=True;MultipleActiveResultSets=true" } 4️⃣ Tạo Database bằng Migration

Mở Package Manager Console và chạy:

Add-Migration InitialCreate Update-Database

Lệnh này sẽ:

Tạo database Tạo các bảng từ model 5️⃣ Chạy ứng dụng

Nhấn:

F5

Hoặc:

Ctrl + F5

Sau đó trình duyệt sẽ mở website của hệ thống.

✅ Yêu cầu hệ thống .NET SDK 6.0 hoặc mới hơn SQL Server Visual Studio 2022 Git
