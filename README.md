Hello and welcome to the sample Product Demo MAUI App where we show how to perform CRUD operations in MAUI app using a local Database (SQLite). 

This video gives a walkthrough of the app as well as a high level walk through of the code https://www.loom.com/share/c9baf3e77b5b43b2826454f7eed07b6f?sid=efd6d5ff-3634-4bac-87ed-34d6ac10a0f1

Below are some screens and functionalities they perform. 

The App Starts with a Blank list of Products 
![Screenshot_1714212798](https://github.com/supreettare/ProductDemoMAUI/assets/284847/47a249fb-3041-4a1b-a580-4dbae0d0dee6)

You can click the + button on the nav bar to add new product. When you click the + button, you are presented with this screen where you can add the Product Details manually. 
The Product image could be captured live using the Camera or an existing image from the Galary can also be uploaded. 
![Screenshot_1714212805](https://github.com/supreettare/ProductDemoMAUI/assets/284847/b90b8668-f98b-4370-a1eb-71f28b292517)
![Screenshot_1714212839](https://github.com/supreettare/ProductDemoMAUI/assets/284847/e89805a5-75e3-4c31-bf43-bb6c30be936b)

Once the product is added, it shows up in the Products List. 
![Screenshot_1714213079](https://github.com/supreettare/ProductDemoMAUI/assets/284847/afec26c4-f295-4e73-bf13-118ccbc58f1d)

When you select any existing Product, the product details screen opens up from where you can Edit the product details or Delete the product altogether. 
![Screenshot_1714213148](https://github.com/supreettare/ProductDemoMAUI/assets/284847/c67b9acb-75fc-4533-a6db-1dcb0caf8df9)

To mimic the buying of a product, we have used an external API https://fakestoreapi.com/products to list products from a dummy online e-commerce store. Using REST APIs we list the products available on this e-commerce store. 
![Screenshot_1714212854](https://github.com/supreettare/ProductDemoMAUI/assets/284847/1c9b5cef-0693-4ab1-bd12-fb094a31f9b1)

When you click + next to a Product in this Product Catalog, you can add this product to your Cart & finally purchase it. For simplicity we are using the selected product details & saving them in our local DB. 
This mimics the Add to Cart & Buy functionalities. 

The code follows MVVM Architecture, uses SQLite for local DB & uses REST APIs to call & interact with external APIs/ Stores. 


