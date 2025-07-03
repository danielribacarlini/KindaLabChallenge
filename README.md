# FoodTruck App

A full-stack web application that allows users to explore food trucks in San Francisco, filter them by category, and view detailed information and menus on an interactive map. This project was developed as part of a technical challenge.

---

## Tech Stack

| Layer            | Technology                                                                 |
| ---------------- | -------------------------------------------------------------------------- |
| Frontend         | [Angular 20](https://angular.io/)                                          |
| Backend          | [.NET 8 Web API](https://dotnet.microsoft.com/)                            |
| Maps             | [@angular/google-maps](https://www.npmjs.com/package/@angular/google-maps) |
| Frontend Tests   | Jasmine + Karma                                                            |
| Backend Tests    | xUnit + Moq                                                                |
| Containerization | Docker + Docker Compose                                                    |

---

## Features

- 🌍 Interactive Google Map showing nearby food trucks.
- 📂 Category filtering (e.g., Pizza, BBQ, Mexican, etc.).
- 🍽 Menu and description for each food truck.
- ⚡ Cached backend responses for improved performance.
- ✅ Unit tests covering both frontend and backend logic.
- 🐳 Dockerized for easy local deployment.

---

## Getting Started

### 1. Clone the repository

```  bash
git clone https://github.com/danielribacarlini/SF-FoodTrucks-locator.git
git checkout food-truck-locator
cd foodtruck
```

---

### 2. Local Development (Debug Mode)

You can run both apps independently for debugging:

#### Backend (.NET 8)

```  bash
cd FoodTruck.Api
dotnet restore
dotnet run
```

The API will be available at: `https://localhost:5000`

#### Frontend (Angular)

```  bash
cd FoodTruck.Web
npm install
ng serve
```

The web app will be available at: `http://localhost:4200`

---

### 3. Dockerized Local Deployment 

From the root of the project, you can build and run everything with Docker Compose:

#### Build the images:

```   bash
docker compose build
```

#### Run the containers:

```   bash
docker compose up
```

#### Result:

- Frontend: [http://localhost:4200]
- Backend Swagger: [http://localhost:5000/swagger/index.html]

---

## Running Tests

### Frontend

```   bash
ng test
```

- Runs unit tests for components like:
  - `FoodTruckMapComponent`
  - `FoodTruckInfoWindowComponent`
- Coverage includes filtering logic, map interaction, and user actions.

### Backend

```   bash
cd backend/FoodTruck.Tests
dotnet test
```

- Covers service logic for filtering and caching.
- Tests HTTP communication and edge cases.

---

## Project Structure

```
../
│
├── FoodTruck/
│   ├── FoodTruck.Api/       # .NET Web API
│   ├── FoodTruck.Tests/     # xUnit tests for the API
│   └── FoodTruck.Web/       # Angular 20 application
│
├── docker-compose.yml       # Orchestrates backend + frontend
├── FoodTruck.sln            # Visual Studio solution file
└── README.md
```

---

## Notes

- The map uses Google Maps JavaScript API. A valid API key is required for production.
- Food truck data is fetched from a public data source provided by the City of San Francisco (with local caching for performance).
- Angular Signals are used for reactive state management in a clean and modern way.

---

## 🧑‍💻 Author

**Daniel Riba**\
Senior .NET & Angular Developer\
📧 danielribacarlini@gmail.com

---

## 📄 License

This project was created exclusively for a technical challenge and is not intended for commercial use.
