# E-Commerce System

An E-Commerce System that was developed as an internship project at adesso Turkey. With the project, customers can;
- Do your shopping while earning a bonus.
- View products by category.
- View product details with customer comments
- Add products to the favorites list.
- Register and log in.
- Manage addresses.
- View transactions and wallet.
- View and cancel orders.
- View and update your profile.

As databases, MSSQL and MongoDB are used.
On the backend side, [.NET](/dotNET/) and [Node.js](/Node/) are used.
The frontend is coded with [React](/React/).

Detailed information is available under each project section.

-------------------------------
## Database Design of the Project

![](/images/database-design.png)

-------------------------------
## Web UI Screenshots
Home Page
![](/images/home-screen.png)

Home Page When User Logged In
![](/images/home-screen-logged-in.png)

Products by Category
![](/images/category-listing.png)

Product Details
![](/images/product-details.png)

Registration
![](/images/register.png)

Login
![](/images/login.png)

Favorites List
![](/images/favorites.png)

Shopping Cart
![](/images/cart.png)

Order Address Selection Page
![](/images/order-address-selection.png)

Orders
![](/images/orders.png)

Addresses
![](/images/addresses.png)

Profile with update options
![](/images/profile.png)

Wallet Details
![](/images/wallet.png)

Toastify Notifications

![](/images/toastify-notifications.png)

-------------------------------
## Running Project with Docker-Compose
- Clone the git repository
- Create a .env file in Node folder and define two variable named
  - DB_CONNECTION => Mongo DB connection string
  - ACCESS_TOKEN_SECRET => a complex string for access token creation
- Open the terminal in project folder
- Run the command below - make sure docker is installed

```  
  docker-compose up --build -d
```
- Open *http://localhost:3000/* and you are good to go :-) 