$(document).ready(function() {
    const component =(product) => `
    <div class="col-xl-4 col-sm-6">
                                <div class="axil-product product-style-one mb--30">
                                    <div class="thumbnail">
                                        <a asp-controller="Product" asp-action="ProductDetail2" asp-route-id="${product.Id}">
                                            <img src="${product.DetailImage}" alt="Product Images">
                                        </a>
                                        <div class="product-hover-action">
                                            <ul class="cart-action">
                                                <li class="wishlist"><a href="wishlist.html"><i class="far fa-heart"></i></a></li>
                                                <li class="select-option"><a asp-controller="Cart" asp-action="AddItemToCart" asp-route-productId="${product.Id}">Add to Cart</a></li>
                                                <li class="quickview"><a href="#" data-bs-toggle="modal" data-bs-target="#quick-view-modal"><i class="far fa-eye"></i></a></li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="product-content">
                                        <div class="inner">
                                            <h5 class="title"><a asp-controller="Product" asp-action="ProductDetail2" asp-route-id="${product.Id}">${product.Name}</a></h5>
                                            <div class="product-price-variant">
                                                <span class="price current-price">$${product.DisplayPrice}</span>
                                                <span class="price old-price">$30</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
    `;
    const getAll = () => {
        let url = `/Product/ProductsJson`;
        $.ajax({
            type: 'GET',
            url: url,
            success: data => console.log(data)
        })
        .fail(error => console.log(error));
    }
    const getPriceLowestToHighest = () => {
        let url = `/Product/ProductsJson`;
        $.ajax({
            type: 'GET',
            url: url,
            success: data => priceLowestToHighest(data)
        })
        .fail(error => console.log(error));
    }
    const getPriceHighestToLowest = () => {
        let url = `/Product/ProductsJson`;
        $.ajax({
            type: 'GET',
            url: url,
            success: data => priceHighestToLowest(data)
        })
        .fail(error => console.log(error));
    }
    const getPriceBetween = (min, max) => {
        let url = `/Product/ProductsJson`;
        $.ajax({
            type: 'GET',
            url: url,
            success: data => priceBetween(data, min, max)
        })
        .fail(error => console.log(error));
    }

    const priceLowestToHighest = (products) => {
        products = products.sort(p => p.DisplayPrice)
        console.log('sorted: ', products)
    }
    const priceHighestToLowest = (products) => {
        products = products.sort(p => p.DisplayPrice)
        products = products.reverse(p => p.DisplayPrice)
        console.log('reversed: ', products)
    }
    const priceBetween = (products, min, max) => {
        products = products.filter(p => p.DisplayPrice <= max & p.DisplayPrice >=min)
        console.log(`between ${min} and ${max}`, products)
    }
    const inCategory = (category) => {
        let products = getAll();

    }
    getPriceLowestToHighest()
    getPriceHighestToLowest()
    getPriceBetween(20, 30)
    
});