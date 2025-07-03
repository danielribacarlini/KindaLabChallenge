# 🚛 FoodTruck App

A full-stack web application that allows users to explore food trucks in San Francisco, filter them by category, and view detailed information and menus on an interactive map. This project was developed as part of KindaLab's technical challenge.

---

## 📆 Tech Stack

| Layer          | Technology                                                                 |
| -------------- | -------------------------------------------------------------------------- |
| Frontend       | [Angular 20](https://angular.io/)                                          |
| Backend        | [.NET 8 Web API](https://dotnet.microsoft.com/)                            |
| Maps           | [@angular/google-maps](https://www.npmjs.com/package/@angular/google-maps) |
| Frontend Tests | Jasmine + Karma                                                            |
| Backend Tests  | xUnit + Moq                                                                |

---

## ✨ Features

- 🌍 Interactive Google Map showing nearby food trucks.
- 📂 Category filtering (e.g., Pizza, BBQ, Mexican, etc.).
- 🍽 Menu and description for each food truck.
- ⚡ Cached backend responses for improved performance.
- ✅ Unit tests covering both frontend and backend logic.

---

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/your-username/foodtruck-app.git
cd foodtruck-app
```

---

### 2. Backend (.NET 8)

#### Requirements:

- .NET SDK 8+
- Visual Studio or VS Code

#### Run the backend:

```bash
cd backend/FoodTruck.API
dotnet restore
dotnet run
```

The API will be available at: `https://localhost:5000`

#### Sample Endpoints:

- `GET /api/foodtrucks`
- `GET /api/foodtrucks/nearby?lat=...&lng=...&categories=...`

#### Swagger UI:

Once running, explore and test endpoints at:

```
http://localhost:5000/swagger/index.html
```

---

### 3. Frontend (Angular 20)

#### Requirements:

- Node.js v18+
- Angular CLI v20+

#### Run the frontend:

```bash
cd frontend/foodtruck-app
npm install
ng serve
```

Visit the app at: `http://localhost:4200`

---

## 🧪 Running Tests

### Frontend

```bash
ng test
```

- Runs unit tests for components like:
  - `FoodTruckMapComponent`
  - `FoodTruckInfoWindowComponent`
- Coverage includes filtering logic, map interaction, and user actions.

### Backend

```bash
cd backend/FoodTruck.Tests
dotnet test
```

- Covers service logic for filtering and caching.
- Tests HTTP communication and edge cases.

---

## 📁 Project Structure

```
foodtruck-app/
│
├── frontend/
│   ├── app/
│   │   ├── pages/
│   │   │   └── food-truck-map/
│   │   ├── components/
│   │   │   └── food-truck-info-window/
│   │   └── services/
│   └── environments/
│
└── backend/
    ├── FoodTruck.API/
    └── FoodTruck.Tests/
```

---

## 📌 Notes

- The map uses Google Maps JavaScript API. A valid API key is required for production.
- Food truck data is fetched from a public data source provided by the City of San Francisco (with local caching for performance).
- Angular Signals are used for reactive state management in a clean and modern way.

---

## 🧑‍💻 Author

**Daniel Riba**\
Senior .NET & Angular Developer\
📧 [Your LinkedIn or email here, if you'd like to include it]

---

## 📄 License

This project was created exclusively for the **KindaLab technical challenge** and is not intended for commercial use.

