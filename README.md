# 🚀 Enterprise Microservices Ecosystem 
[![Status](https://img.shields.io/badge/Status-Work_in_Progress-orange.svg)]()

> **Özet:** İş süreçlerini merkezi ve esnek bir yapıda yönetmek üzere tasarlanmış, olay güdümlü (event-driven) bir mikroservis ekosistemidir. Merkezde yer alan Workflow Engine, Identity ve Reporting gibi çekirdek servisler, sisteme sonradan eklenecek herhangi bir yeni sürecin mevcut altyapıyla sorunsuz ve entegre bir şekilde çalışmasına olanak tanır. Proje şu anda aktif geliştirme aşamasındadır.

---

## 🏛️ Mimari ve Tasarım Prensipleri

Sistem, yeni servislerin minimum eforla ekosisteme dahil edilebilmesi ve yüksek sürdürülebilirlik ilkeleri göz önünde bulundurularak tasarlanmıştır.

* **Esnek Servis Entegrasyonu:** İş süreçleri (Business Domains), merkezi sistemlerden izole edilmiştir. Yeni bir süreç eklendiğinde, sadece message broker üzerinden yayınlanan olaylara (events) abone olarak merkezi servislerle anında entegre olabilir.
* **Mimari Desenler:** Servisler, **Onion Architecture** ve **Domain-Driven Design (DDD)** prensiplerine sadık kalınarak katmanlandırılmıştır.
* **Veri Yönetimi ve İzolasyon:** Okuma ve yazma operasyonlarının ayrıştırılması ve sistem esnekliğinin artırılması için **CQRS** deseni uygulanmıştır.
* **Asenkron İletişim:** Servisler arası bağımlılıkları ortadan kaldırmak için mesaj kuyruğu kullanılarak olay güdümlü (event-driven) bir yapı inşa edilmiştir.

---

## 💻 Teknoloji Yığını (Tech Stack)

* **Backend:** .NET 9.0 (C#)
* **Merkezi Kimlik Yönetimi:** Duende Identity Server
* **Gerçek Zamanlı İletişim:** SignalR (Planlanıyor)
* **Mesajlaşma (Event Bus):** RabbitMQ
* **Container:** Docker & Docker Compose
* **Veritabanları:** PostgreSQL

---

## ⚙️ Çekirdek Servisler (Core Services)

Bu servisler, ekosisteme dahil olan tüm iş modüllerine merkezi hizmet sağlar:

* **Workflow Engine Service:** Sistemdeki tüm iş akışlarını ve durum (state) geçişlerini yöneten, özel olarak tasarlanmış olay güdümlü mikroservis.
* **Identity Service:** Duende Identity Server altyapısıyla tüm ekosistemin merkezi kimlik doğrulama ve yetkilendirmesini sağlar.
* **Reporting Service (WIP):** Sisteme bağlı tüm servislerden gelen verileri konsolide ederek raporlanabilir hale getiren yapı.

## 🏢 İş Süreci Servisleri (Business Domain Services)

* **HealthCare Service (Aktif):** Sağlık sektörü senaryolarını ve domain kurallarını barındıran modül.
* **Project Tracking Service (Planlanıyor):** Şirket içi görev ve proje takibi için eklenecek yeni modül.

---

## 🚀 Hızlı Başlangıç (Getting Started)

**Ön Koşullar:**
* Docker Desktop'ın kurulu ve çalışır durumda olması.

**Çalıştırma Adımları:**

Projeyi klonlamak ve tüm altyapıyı ayağa kaldırmak için aşağıdaki komutları tek seferde kopyalayıp terminalinizde çalıştırabilirsiniz:

```bash
git clone https://github.com/dogukanderici/HealthCare-Microservices-Architecture.git
cd HealthCare-Microservices-Architecture
docker-compose up -d --build
```
---

> (Not: Proje geliştirme aşamasında olduğundan, servislerin portları ve API uç noktaları güncellenebilir. Detaylı Swagger API dökümantasyonu için yerel sunucunuzdaki ilgili portları kontrol ediniz.)
