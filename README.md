<div align="center">

# 🛡️ InsureYouAi
### Yapay Zeka Destekli Sigorta Yönetim ve Danışmanlık Platformu

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-MVC-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/apps/aspnet)
[![Entity Framework](https://img.shields.io/badge/EF_Core-ORM-3F51B5?style=flat-square)](https://learn.microsoft.com/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-Database-CC2927?style=flat-square&logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)
[![SignalR](https://img.shields.io/badge/SignalR-Realtime-black?style=flat-square)](https://dotnet.microsoft.com/apps/aspnet/signalr)
[![OpenAI](https://img.shields.io/badge/OpenAI-GPT--4o-412991?style=flat-square&logo=openai)](https://platform.openai.com/)
[![Claude](https://img.shields.io/badge/Anthropic-Claude-D97757?style=flat-square)](https://www.anthropic.com/)
[![Hugging Face](https://img.shields.io/badge/Hugging_Face-Toxic--BERT-FFD21E?style=flat-square&logo=huggingface)](https://huggingface.co/)
[![ElevenLabs](https://img.shields.io/badge/ElevenLabs-TTS-000000?style=flat-square)](https://elevenlabs.io/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3?style=flat-square&logo=bootstrap)](https://getbootstrap.com/)

*Admin paneli, üye alanı ve beş farklı yapay zeka servisinin (OpenAI, Claude, Hugging Face, ElevenLabs, Tavily) entegre edildiği katmanlı bir sigorta yönetim sistemi.*

</div>

---

## 📋 İçindekiler

- [Genel Bakış](#-genel-bakış)
- [Öne Çıkan Özellikler](#-öne-çıkan-özellikler)
- [Kullanılan Teknolojiler](#️-kullanılan-teknolojiler)
- [Proje Yapısı](#-proje-yapısı-özet)
- [Ekran Görüntüleri](#-ekran-görüntüleri)
- [Kurulum](#️-kurulum)
- [Kimlik Doğrulama & Roller](#-kimlik-doğrulama--roller)

---

## 📖 Genel Bakış

**InsureYouAi**, sigorta şirketlerinin dijital operasyonlarını tek bir çatı altında yönetebilmesi için geliştirilmiş kapsamlı bir web platformudur. Sistem üç ana kullanıcı deneyimi sunar:

- **Genel Site (Public)** — ziyaretçilere açık ana sayfa, blog, hizmetler, hakkımızda ve sigorta talep formu gibi kurumsal tanıtım sayfaları.
- **Yönetim Paneli (Admin)** — içerik, kullanıcı, poliçe, rol ve mesaj yönetiminin yapıldığı kontrol merkezi.
- **Üye Paneli (Member Area)** — poliçe sahiplerinin kendi poliçelerini, makalelerini ve profillerini görüntüleyebildiği kişisel alan.

Platformun ayırt edici özelliği, birden fazla yapay zeka servisinin iş akışlarına organik olarak entegre edilmiş olmasıdır — sigorta önerisinden poliçe doküman analizine, sesli asistandan gerçek zamanlı sohbete ve otomatik yorum moderasyonuna kadar geniş bir AI destekli özellik yelpazesi sunulur.

---

## ✨ Öne Çıkan Özellikler

### 🏢 Yönetim Paneli

- İçerik yönetimi: Makaleler, Kategoriler, Hakkımızda, Hizmetler, Slider, Galeri, Referanslar, Tanıtım Videoları
- Fiyat planları ve plan öğeleri yönetimi (öne çıkan plan işaretleme, aktif/pasif durum)
- Mesaj ve iletişim formu yönetimi (okundu/okunmadı işaretleme, AI destekli otomatik yanıt önerisi)
- Kullanıcı ve rol yönetimi (ASP.NET Identity tabanlı, dinamik rol atama arayüzü)
- Bülten (Newsletter) abone yönetimi
- Özel tasarlanmış 403 / 404 hata sayfaları

### 👤 Üye Paneli (Member Area)

- Kişiselleştirilmiş Dashboard: aktif poliçe sayısı, toplam yıllık prim, en yakın yenileme tarihi, poliçe türü dağılımı
- Poliçelerim: durum bazlı (aktif/süresi dolmuş) renkli listeleme
- Makalelerim: kullanıcıya ait/ilişkili makale listesi ve detay sayfası
- Profil yönetimi ve şifre değiştirme
- Kurumsal, özgün tasarım dili (ink-navy + bronz vurgu renk paleti, Fraunces/Inter font ikilisi)

### 🤖 Yapay Zeka Entegrasyonları

| Özellik | Kullanılan Servis | Açıklama |
|---|---|---|
| AI Sigorta Önerisi | OpenAI (GPT-4o-mini) | Kullanıcı profiline göre en uygun poliçe planını önerir |
| AI ile Makale Oluşturma | OpenAI (GPT) | Belirli bir konu/başlık üzerinden otomatik blog içeriği üretir |
| PDF Poliçe Analizi | Anthropic Claude | Yüklenen poliçe PDF'ini özetler, kapsam/istisna analizi yapar |
| Otomatik Mail Yanıt Önerisi | Anthropic Claude | Gelen müşteri mesajlarına AI destekli taslak yanıt üretir, yanıt geçmişini tutar |
| Metin Seslendirme | ElevenLabs | Poliçe özetlerini veya herhangi bir metni sesli çıktıya dönüştürür |
| AI ile Ara ve Yanıtla | Tavily | Web arama destekli soru-cevap |
| Gerçek Zamanlı AI Sohbet | OpenAI + SignalR | Streaming (token-by-token) canlı sohbet deneyimi |
| Kullanıcı Bazlı Makale Analizi | OpenAI (GPT) | Bir kullanıcının yazdığı makaleleri analiz ederek içerik/üslup değerlendirmesi sunar |
| Yorum Bazlı Karakter Analizi | OpenAI (GPT) | Kullanıcının yaptığı yorumlardan yola çıkarak davranış/karakter profili çıkarımı yapar |
| Otomatik Yorum Moderasyonu | Hugging Face (Toxic-BERT) | Yorumları Türkçe → İngilizce çeviri hattı üzerinden toksisite analizinden geçirir; uygunsuz içerik otomatik engellenir |

---

## 🛠️ Kullanılan Teknolojiler

### Backend

- **.NET 9 / ASP.NET Core MVC** — Ana uygulama çatısı
- **Entity Framework Core** — ORM ve veritabanı erişim katmanı
- **Microsoft SQL Server** — İlişkisel veritabanı
- **ASP.NET Core Identity** — Kimlik doğrulama, yetkilendirme, rol yönetimi
- **AutoMapper** — DTO ↔ Entity nesne eşleme
- **SignalR** — Gerçek zamanlı, çift yönlü iletişim (canlı sohbet)

### Frontend

- **Razor Views (.cshtml)** — Sunucu taraflı görünüm katmanı
- **Bootstrap 5** — Admin panel arayüz çatısı
- **Bootstrap Icons** — İkon kütüphanesi (admin panel)
- **Özel CSS Tasarım Sistemi** — Üye paneli için sıfırdan tasarlanmış, framework'ten bağımsız stil katmanı
- **Inline SVG İkonografi** — Font/CDN bağımlılığı olmadan render edilen ikon seti
- **Google Fonts (Fraunces + Inter)** — Üye paneli tipografi ikilisi
- **Vanilla JavaScript** — Etkileşim katmanı (arama filtreleme, modal yönetimi, form davranışları)

### Yapay Zeka & Harici Servisler

- **OpenAI API** (Chat Completions, Streaming, DALL·E) — Sigorta önerisi, sohbet, makale/görsel üretimi
- **Anthropic Claude API** — PDF doküman analizi, otomatik mail yanıt önerisi
- **ElevenLabs API** — Text-to-Speech (metin seslendirme)
- **Tavily API** — AI destekli web arama
- **Hugging Face Inference API** (Helsinki-NLP/opus-mt-tr-en, unitary/toxic-bert) — Yorum moderasyonu için çeviri ve toksisite tespiti

### Doküman & Dosya İşleme

- **UglyToad.PdfPig** — PDF içerik/metin çıkarma
- **QuestPDF** — Programatik PDF doküman üretimi

### Mimari Desenler

- **Area-based Modularization** — Admin ve Member alanlarının route/klasör seviyesinde ayrıştırılması
- **Service Layer Pattern** (`IService` arayüzleri + `Concrete` implementasyonları)
- **Generic Repository/Service** yapısı (`GenericService<T>`)
- **ViewComponent Mimarisi** — Dashboard widget'ları, layout parçaları (Sidebar, Header, Footer) için yeniden kullanılabilir bileşenler
- **DTO Pattern** — Katmanlar arası veri taşıma nesneleri (Create/Update/Result varyantları)

---

## 📁 Proje Yapısı (Özet)

```
InsureYouAi/
├── Areas/
│   └── Member/
│       ├── Controllers/       # Üye paneli controller'ları (Dashboard, MemberArticle, MemberPolicies, MemberProfile, Account)
│       ├── ViewComponents/     # Dashboard widget'ları (PolicyAmount, NearestExpiry, UserArticle, PolicyType vb.)
│       └── Views/
│           └── Shared/
│               ├── _MemberLayout.cshtml
│               └── Components/    # ViewComponent view'ları
├── Controllers/                # Admin panel controller'ları
├── Dtos/                        # Katman-arası veri transfer nesneleri
├── Entities/                    # Veritabanı entity sınıfları (AppUser, AppRole, Article, Policy, PricingPlan vb.)
├── Service/
│   ├── Interfaces/              # Servis sözleşmeleri
│   └── Concrete/                # Servis implementasyonları
├── Models/                      # Hub'lar (ChatHub) ve ViewModel'ler
├── Context/                     # EF Core DbContext
├── Views/                       # Admin panel Razor view'ları
└── wwwroot/                     # Statik dosyalar (CSS, JS, görseller)
```

---

## 🖼 Ekran Görüntüleri

<details open>
<summary><b>🌐 Genel Site (Public)</b></summary>
<br>

**Ana Sayfa – Slider**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Ana Sayfa Slider.png" alt="Ana Sayfa Slider" width="100%">

**Ana Sayfa – Hakkımızda**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Ana Sayfa Hakkıımızda.png" alt="Ana Sayfa Hakkımızda" width="100%">

**Ana Sayfa – Video & Servisler**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Ana Sayfa Video & Servis.png" alt="Ana Sayfa Video ve Servis" width="100%">

**Ana Sayfa – İstatistikler**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Ana Sayfa Istatistikler.png" alt="Ana Sayfa İstatistikler" width="100%">

**Ana Sayfa – Ödeme Planları**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Ana Sayfa Ödeme Planları.png" alt="Ana Sayfa Ödeme Planları" width="100%">

**Ana Sayfa – Galeri & Blog**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Ana Sayfa Galeri & Blog.png" alt="Ana Sayfa Galeri ve Blog" width="100%">

**Ana Sayfa – Sigorta Talebi**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Ana Sayfa Sigorta Talebi.png" alt="Ana Sayfa Sigorta Talebi" width="100%">

**Blog Listesi**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Blog Listesi.png" alt="Blog Listesi" width="100%">

**Blog Detay**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Blog Detay.png" alt="Blog Detay" width="100%">

**Blog Detay – Yorumlar**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Blog Detay Yorum.png" alt="Blog Detay Yorum" width="100%">

**Giriş Sayfası**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Login Sayfası.png" alt="Login Sayfası" width="100%">

</details>

<details open>
<summary><b>🔐 Admin Panel</b></summary>
<br>

**Admin Dashboard**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Admin Dashboard.png" alt="Admin Dashboard" width="100%">

**Admin Dashboard (Detay)**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Admin Dashboard 2.png" alt="Admin Dashboard 2" width="100%">

**Kullanıcı Rol Atama Sayfası**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Kullanıcı Rol Atama Sayfası.png" alt="Kullanıcı Rol Atama Sayfası" width="100%">

</details>

<details open>
<summary><b>🤖 Yapay Zeka Özellikleri</b></summary>
<br>

**AI ile Makale Oluşturma**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/AI İle Makale Oluşturma.png" alt="AI İle Makale Oluşturma" width="100%">

**AI ile Sigorta Önerisi**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Ai İle Sigorta Önerisi.png" alt="AI İle Sigorta Önerisi" width="100%">

**PDF Poliçe Analizi**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Pdf Analizi.png" alt="PDF Analizi" width="100%">

**Claude ile Mail Yanıt Önerisi**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Claude Mail Yanıt Önerisi.png" alt="Claude Mail Yanıt Önerisi" width="100%">

**Claude Yanıt Geçmişi**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Claude Yanıt Geçmişi.png" alt="Claude Yanıt Geçmişi" width="100%">

**Hugging Face ile Yorum Kontrolü**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Hugging Face Yorum Kontrolü.png" alt="Hugging Face Yorum Kontrolü" width="100%">

**Makaleye Göre Kullanıcı Analizi**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Makaleye Göre Kullanııcı Analizi.png" alt="Makaleye Göre Kullanıcı Analizi" width="100%">

**Yorumlara Göre Kullanıcı Analizi**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Yorumlara  Göre Kullanıcı Analizi.png" alt="Yorumlara Göre Kullanıcı Analizi" width="100%">

**Metni Sese Çevirme (ElevenLabs)**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Metni Sese Çevirme.png" alt="Metni Sese Çevirme" width="100%">

**Tavily AI ile Metin Arama**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Tavily Ai İle Metin Arama.png" alt="Tavily AI İle Metin Arama" width="100%">

**SignalR ile Anlık Chatleşme**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/SignalR Anlık Chatleşme.png" alt="SignalR Anlık Chatleşme" width="100%">

</details>

<details open>
<summary><b>👤 Üye Paneli</b></summary>
<br>

**Üye Paneli – Dashboard**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Üye Panel.png" alt="Üye Panel" width="100%">

**Üye Paneli – Poliçelerim**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Üye Poliçe.png" alt="Üye Poliçe" width="100%">

**Üye Paneli – Makalelerim**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Üye Makaleler.png" alt="Üye Makaleler" width="100%">

**Üye Paneli – Profilim**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/Üye Profil.png" alt="Üye Profil" width="100%">

</details>

<details open>
<summary><b>⚠️ Hata Sayfaları</b></summary>
<br>

**403 – Erişim Engellendi**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/403 Sayfası.png" alt="403 Sayfası" width="100%">

**404 – Sayfa Bulunamadı**
<img src="InsureYouAi/InsureYouAi/wwwroot/images/404 Sayfası.png" alt="404 Sayfası" width="100%">

</details>

---

## ⚙️ Kurulum

### Gereksinimler

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- Microsoft SQL Server (LocalDB veya tam sürüm)
- Visual Studio 2022 / VS Code

### Adımlar

1. **Depoyu klonlayın**

```bash
git clone https://github.com/EmreHaci03/InsureYouAI.git
cd InsureYouAI
```

2. **appsettings.json yapılandırması**

Bağlantı dizesi ve API anahtarlarını tanımlayın:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=InsureAiDb;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "ApiKey": {
    "OpenAi": "sk-...",
    "Claude": "sk-ant-...",
    "HuggingFace": "hf_...",
    "Gemini": "...",
    "TavilyAi": "tvly-..."
  },
  "ElevenLabs": {
    "ApiKey": "sk_...",
    "VoiceId": "..."
  },
  "MailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SenderEmail": "admin@ornek.com",
    "SenderName": "InsureAI Admin",
    "SenderPassword": "uygulama-şifresi"
  }
}
```

> ⚠️ API anahtarlarını asla kaynak kontrolüne (Git) dahil etmeyin. Geliştirme ortamında `dotnet user-secrets` kullanılması önerilir. Gmail SMTP için normal hesap şifresi değil, Google hesabında oluşturulan **Uygulama Şifresi (App Password)** kullanılmalıdır.

3. **Veritabanı migration'larını uygulayın**

```bash
dotnet ef database update
```

4. **Paketleri geri yükleyin ve çalıştırın**

```bash
dotnet restore
dotnet run
```

5. Tarayıcıda konsolda belirtilen adrese gidin.

---

## 🔐 Kimlik Doğrulama & Roller

Sistem, ASP.NET Core Identity üzerine kurulu özel `AppUser` ve `AppRole` sınıflarını kullanır. Kullanıcılar tek bir giriş ekranından (`Account/Login`) sisteme dahil olur; kimlik doğrulaması başarılı olduğunda **kullanıcının sahip olduğu role bakılarak** otomatik yönlendirme yapılır:

- **Admin** rolüne sahip kullanıcılar → Yönetim Paneli (`Dashboard/Index`)
- Diğer tüm kullanıcılar (üye/poliçe sahibi) → Üye Paneli (`Areas/Member/Dashboard/Index`)

Bu sayede tek bir `AccountController` üzerinden hem yöneticiler hem de üyeler giriş yapabilir, admin panelinin ve üye panelinin ayrı giriş ekranlarına ihtiyaç duyulmaz. Rol yönetimi, admin panelindeki **Roller** modülünden dinamik olarak yapılır (rol oluşturma, düzenleme, kullanıcıya rol atama).

---

<div align="center">

**InsureYouAi** · *Güveniniz bizim güvencemiz.*

</div>
