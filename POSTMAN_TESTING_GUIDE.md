# Postman Testing Guide for ContosoPizza API

## Base URL
```
http://localhost:5252
```

## Important: Start the Application First
Before testing, run the application:
```bash
dotnet run --launch-profile http
```

---

## 1. GET ALL PIZZAS
**Method:** GET  
**URL:** `http://localhost:5252/Pizza`  
**Description:** Retrieve all pizzas from the database (includes 6 seed pizzas)

**Expected Response (200 OK):**
```json
[
  {
    "id": 1,
    "name": "Classic Margherita",
    "description": "Fresh tomato sauce, mozzarella, and basil",
    "price": 12.99,
    "isGlutenFree": false,
    "size": 1,
    "toppings": null,
    "imageUrl": "https://example.com/margherita.jpg"
  },
  ...
]
```

---

## 2. GET PIZZA BY ID
**Method:** GET  
**URL:** `http://localhost:5252/Pizza/1`  
**Description:** Retrieve a specific pizza by ID

**Expected Response (200 OK):**
```json
{
  "id": 1,
  "name": "Classic Margherita",
  "description": "Fresh tomato sauce, mozzarella, and basil",
  "price": 12.99,
  "isGlutenFree": false,
  "size": 1,
  "toppings": null,
  "imageUrl": "https://example.com/margherita.jpg"
}
```

**Test with invalid ID:** `http://localhost:5252/Pizza/999`  
**Expected Response:** 404 Not Found

---

## 3. CREATE NEW PIZZA
**Method:** POST  
**URL:** `http://localhost:5252/Pizza`  
**Headers:**
```
Content-Type: application/json
```

**Body (Full Example):**
```json
{
  "name": "Spicy Italian",
  "description": "Hot peppers, Italian sausage, and extra spicy sauce",
  "price": 17.99,
  "isGlutenFree": false,
  "size": 2,
  "toppings": ["Peppers", "Sausage", "Onions", "Hot Sauce"],
  "imageUrl": "https://example.com/spicy.jpg"
}
```

**Body (Minimal Valid Example):**
```json
{
  "name": "Simple Cheese",
  "price": 9.99,
  "size": 1
}
```

**Expected Response (201 Created):**
```json
{
  "id": 7,
  "name": "Spicy Italian",
  "description": "Hot peppers, Italian sausage, and extra spicy sauce",
  "price": 17.99,
  "isGlutenFree": false,
  "size": 2,
  "toppings": ["Peppers", "Sausage", "Onions", "Hot Sauce"],
  "imageUrl": "https://example.com/spicy.jpg"
}
```

**Response Headers will include:**
```
Location: http://localhost:5252/Pizza/7
```

### Size Enum Values:
- `0` = Small
- `1` = Medium
- `2` = Large
- `3` = ExtraLarge

### Validation Test - Invalid Data:
**Body:**
```json
{
  "name": "X",
  "price": -5,
  "size": 1
}
```

**Expected Response (400 Bad Request):**
```json
{
  "errors": {
    "Name": ["Name must be between 2 and 100 characters"],
    "Price": ["Price must be between $0.01 and $999.99"]
  }
}
```

---

## 4. UPDATE PIZZA
**Method:** PUT  
**URL:** `http://localhost:5252/Pizza/1`  
**Headers:**
```
Content-Type: application/json
```

**Body:**
```json
{
  "id": 1,
  "name": "Classic Margherita - Updated",
  "description": "Fresh tomato sauce, premium mozzarella, and fresh basil",
  "price": 14.99,
  "isGlutenFree": false,
  "size": 2,
  "toppings": ["Tomato", "Mozzarella", "Basil", "Olive Oil"],
  "imageUrl": "https://example.com/margherita-updated.jpg"
}
```

**Important:** The `id` in the URL must match the `id` in the body!

**Expected Response:** 204 No Content

**Test ID Mismatch:**
- URL: `http://localhost:5252/Pizza/1`
- Body id: `2`
- **Expected Response:** 400 Bad Request - "ID mismatch"

**Test Update Non-Existent Pizza:**
- URL: `http://localhost:5252/Pizza/999`
- **Expected Response:** 404 Not Found

---

## 5. DELETE PIZZA
**Method:** DELETE  
**URL:** `http://localhost:5252/Pizza/1`  
**Description:** Delete a pizza by ID

**Expected Response:** 204 No Content

**Test Delete Non-Existent Pizza:**
- URL: `http://localhost:5252/Pizza/999`
- **Expected Response:** 404 Not Found

---

## Complete Test Workflow

### Step 1: Get all pizzas (should show 6 seed pizzas)
```
GET http://localhost:5252/Pizza
```

### Step 2: Get a specific pizza
```
GET http://localhost:5252/Pizza/1
```

### Step 3: Create a new pizza
```
POST http://localhost:5252/Pizza
Content-Type: application/json

{
  "name": "Four Cheese Delight",
  "description": "Mozzarella, Parmesan, Gorgonzola, and Ricotta",
  "price": 16.99,
  "isGlutenFree": false,
  "size": 2,
  "toppings": ["Mozzarella", "Parmesan", "Gorgonzola", "Ricotta"],
  "imageUrl": "https://example.com/four-cheese.jpg"
}
```

### Step 4: Verify the new pizza was created (should now show 7 pizzas)
```
GET http://localhost:5252/Pizza
```

### Step 5: Update the newly created pizza (use the ID from Step 3 response)
```
PUT http://localhost:5252/Pizza/7
Content-Type: application/json

{
  "id": 7,
  "name": "Four Cheese Deluxe - UPDATED",
  "description": "Premium four cheese blend with truffle oil",
  "price": 19.99,
  "isGlutenFree": false,
  "size": 3,
  "toppings": ["Mozzarella", "Parmesan", "Gorgonzola", "Ricotta", "Truffle Oil"],
  "imageUrl": "https://example.com/four-cheese-deluxe.jpg"
}
```

### Step 6: Verify the update
```
GET http://localhost:5252/Pizza/7
```

### Step 7: Delete the pizza
```
DELETE http://localhost:5252/Pizza/7
```

### Step 8: Verify deletion (should return 404)
```
GET http://localhost:5252/Pizza/7
```

---

## Swagger UI Alternative

You can also test the API using Swagger UI:
```
http://localhost:5252/swagger
```

This provides an interactive interface to test all endpoints without Postman!

---

## Troubleshooting

1. **Connection Refused:** Make sure the application is running (`dotnet run`)
2. **404 on all endpoints:** Check that you're using the correct base URL and port
3. **Database errors:** Verify PostgreSQL is running and the connection string in `appsettings.json` is correct
4. **Validation errors:** Check that required fields (name, price, size) are included and meet the validation criteria
