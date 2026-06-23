# La SAUCELERIE
A full-stack restaurant ordering application built as a 4-person team project, with the primary focus on backend architecture: a secure, database-connected REST API built with ASP.NET Core.

# Project Status
This was an academic project. The database was hosted on a school-provided account, and access was lost after the course ended, so the live backend/API is no longer running. The code remains available below for review.

# Project Overview
This project simulates an online restaurant ordering system. While the team collaborated on the full stack, the main objective was to design and implement a robust backend: building a RESTful API in ASP.NET Core, connecting it to a relational database, and securing user authentication beyond the basics.

# Features
- Two-Factor Authentication (2FA) — added a second verification step on top of standard login to secure user accounts
- CAPTCHA verification — protects login/signup forms from bots and automated abuse
- Dynamic search bar — real-time filtering of menu items as the user types
- Dynamic shopping cart — live cart updates (add/remove/quantity) without full page reloads
- REST API backend — built with ASP.NET Core, connected to a relational database for menu, user, and order data

# Tech Stack 
# Layer             Technology
-----------------------------------------------
  Backend           ASP.NET Core (C#)
