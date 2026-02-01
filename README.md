# Curated Tweets – Twitter Automation (ASP.NET Core)

🔗 **Live App**: https://tweet-automate-zz64.onrender.com/

This project is a lightweight **ASP.NET Core Razor Pages** application that fetches, filters, ranks, and displays **curated tweets from X (Twitter)**.  
The focus is on **relatable, high-engagement developer content** rather than purely technical posts.

---

## 🚀 What This App Does

- Fetches tweets using **X (Twitter) API v2 – Recent Search**
- Uses a carefully designed search query to find relatable developer posts
- Filters out low-quality, spammy, or irrelevant tweets
- Ranks tweets based on engagement (likes, replies)
- Caches results to avoid exhausting free-tier API limits
- Displays curated tweets in a clean, mobile-friendly UI
- Deployed **for free** on **Render**


## 🏗️ Tech Stack

- **ASP.NET Core 8**
- **Razor Pages**
- **X (Twitter) API v2**
- **In-memory caching**
- **Bootstrap**
- **Docker**
- **Render (Free Tier)**

## 📁 Project Structure
```
TwitterAutomation
│
├── Pages
│ ├── Index.cshtml
│ ├── Index.cshtml.cs
│ └── Privacy.cshtml
│
├── Procurement
│ └── TwitterClient.cs
│
├── Processing
│ ├── TweetFilter.cs
│ └── TweetRanker.cs
│
├── Models
│ └── TweetDto.cs
│
├── wwwroot
│ └── css / js
│
├── Program.cs
├── Dockerfile
└── README.md
```
## 🧑‍💻 How to Use This Project

You can run this project **locally** or **deploy it for free** (just like the live version) by following the steps below.

### 1️⃣ Prerequisites

Make sure you have:

- **.NET SDK 8.0+**
- **Docker** (optional, for deployment)
- An **X (Twitter) Developer Account**
- A **Bearer Token** from the X Developer Portal


### 2️⃣ Get a Twitter (X) Bearer Token

1. Go to the **X Developer Portal**
2. Create or select a project/app
3. Copy the **Bearer Token**

### 3️⃣ Clone the Repository

```bash
git clone https://github.com/your-username/your-repo-name.git
cd your-repo-name
```
Add this in your appsettings.json
```
{
  "Twitter": {
    "BearerToken": "your_bearer_token_here"
  }
}
```
Run your app locally 
```
dotnet restore
dotnet run
```

---

## Authors

- [Krish Panchal](https://github.com/beastkp)


