# MultiShop - .NET Core Microservice E-Commerce Project 🚀

![.NET Core](https://img.shields.io/badge/.NET%20Core-6.0%2F7.0%2F8.0-purple)
![Architecture](https://img.shields.io/badge/Architecture-Microservices-blue)
![Pattern](https://img.shields.io/badge/Pattern-CQRS%20%26%20Mediator-green)
![License](https://img.shields.io/badge/License-MIT-orange)

## 📖 Proje Hakkında

Bu proje, **.NET Core** kullanılarak geliştirilmiş kapsamlı bir **Mikroservis E-Ticaret** platformudur. Üniversite ortamında geliştirilen bu uygulama, modern yazılım mimarilerini ve endüstri standartlarını (Soğan Mimarisi, CQRS, Event-Driven Design) pratik bir senaryo üzerinde uygulamayı hedefler.

Proje; bağımsız deploy edilebilen servislerden, merkezi bir kimlik doğrulama yapısından ve kullanıcı dostu bir arayüzden oluşmaktadır.

---

## 🏗️ Mimari ve Teknolojiler (Architecture & Tech Stack)

Projede **Microservice Architecture** benimsenmiş olup, servisler arası iletişim ve veri tutarlılığı için güncel teknolojiler kullanılmıştır.

### 🔧 Backend Teknolojileri
* **Framework:** .NET Core / ASP.NET Core Web API
* **Mimari Yaklaşım:** Onion Architecture (Soğan Mimarisi)
* **Veri Erişimi:** Entity Framework Core (Code First)
* **Tasarım Desenleri:** CQRS (Command Query Responsibility Segregation), Mediator Pattern (MediatR), Repository Pattern
* **Veritabanları:** MSSQL Server (Order, Cargo, Auth vb.), MongoDB (Catalog), Redis (Basket/Sepet)
* **İletişim:** HTTP (Senkron), RabbitMQ (Asenkron - Message Broker)
* **API Gateway:** Ocelot
* **Kimlik Doğrulama:** IdentityServer4 / JWT (JSON Web Token)
* **Konteynerizasyon:** Docker & Docker Compose

### 💻 Frontend Teknolojileri
* **UI Framework:** ASP.NET Core MVC
* **Tasarım:** HTML5, CSS3, Bootstrap
* **İstemci İletişimi:** AJAX, JQuery, HttpClient
* **Template:** Admin ve User panelleri için Responsive temalar

---

## 🧩 Mikroservisler (Microservices)

Proje aşağıdaki temel mikroservislerden oluşmaktadır:

1.  **📂 Catalog Service:** Ürünlerin, kategorilerin ve ürün görsellerinin yönetildiği servis (MongoDB tabanlı).
2.  **🛒 Basket Service:** Kullanıcıların sepet işlemlerini yönetir, hızlı erişim için Redis kullanır.
3.  **📦 Order Service:** Sipariş oluşturma, adres yönetimi ve sipariş detaylarını işleyen servis (CQRS & Mediator yapısında).
4.  **🚚 Cargo Service:** Kargo takip ve durum güncellemeleri.
5.  **💰 Discount Service:** Kupon ve indirim kodlarının yönetimi.
6.  **💳 Payment Service:** Ödeme işlemlerinin simülasyonu.
7.  **🔐 Identity Service:** Kullanıcı kayıt, giriş ve Token (JWT) üretimi.
8.  **💬 Comment Service:** Kullanıcı yorumları ve değerlendirmeleri.

---

## 🚀 Kurulum ve Çalıştırma (Getting Started)

Projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyebilirsiniz.

### Gereksinimler
* .NET SDK
* SQL Server
* MongoDB
* Redis
* Docker (Opsiyonel ama önerilir)

### Adımlar

1.  **Projeyi Klonlayın:**
    ```bash
    git clone [https://github.com/yusuferturkk/ECommerce_Microservice.git](https://github.com/yusuferturkk/ECommerce_Microservice.git)
    ```

2.  **Veritabanı Ayarları:**
    Her servisin `appsettings.json` dosyasındaki Connection String alanlarını kendi yerel veritabanı ayarlarınıza göre güncelleyin.

3.  **Migration İşlemleri:**
    SQL Server kullanan servisler (Order, Identity vb.) için Package Manager Console üzerinden migration uygulayın:
    ```bash
    Update-Database
    ```

4.  **Projeyi Ayağa Kaldırma:**
    Çoklu başlangıç projesi olarak ayarlayarak veya Docker Compose kullanarak servisleri başlatın.

---

## 📸 Ekran Görüntüleri (Screenshots)

*(Buraya projenin arayüzünden, özellikle Admin paneli, Sepet sayfası, Sipariş Detay Modal'ı gibi kısımlardan ekran görüntüleri ekleyebilirsin. Örn: `/Screenshots/order-detail.png`)*

| Ana Sayfa | Sipariş Detayı |
| :---: | :---: |
| ![Home Page](https://via.placeholder.com/400x200?text=Ana+Sayfa+Resmi) | ![Order Detail](https://via.placeholder.com/400x200?text=Siparis+Detay+Resmi) |
