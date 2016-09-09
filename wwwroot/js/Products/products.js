//model
var Product = function (description, cost, inventory) {
  this.id = Math.random().toString().split('.')[1];
  this.description = description;
  this.cost = cost;
  this.inventory = inventory;
};

Product.prototype.isInStock = function () {
  return this.inventory && this.inventory > 0;
};

//respository
var ProductRepository = function (products) {
  this.products = products;
};

ProductRepository.prototype.add = function (product) {
  this.products.push(product);
};

ProductRepository.prototype.getIndexForProductId = function (id) {
  for (var i = 0; i < this.products.length; i++) {
    if (this.products[i].id == id) {
      return i;
    }
  }

  return 0;
};

ProductRepository.prototype.find = function (id) {
  return this.products[this.getIndexForProductId(id)]
};

ProductRepository.prototype.getAll = function () {
  return this.products;
};

ProductRepository.prototype.delete = function (id) {
  var index = this.getIndexForProductId(id);

  if (index && index > -1) {
    this.products.splice(index, 1);
  }
};

var myPage = {
  repository: new ProductRepository([
    new Product('T-Shirt', 10.99, 25),
    new Product('Sticker', 8.99, 49),
    new Product('Sweat Band', 9.99, 0)
  ]
  ),
  vueProductList: Vue.extend({
    template: '#product-list',
    data: function () {
      return {
        products: myPage.repository.getAll(),
        tableSortField: 'description',
      };
    }
  }),
  vueAddProduct: Vue.extend({
    template: '#add-product',
    data: function () {
      return {
        product: {
          description: '',
          cost: null,
          inventory: null,
        }
      }
    },
    methods: {
      createProduct(e) {
        e.preventDefault();
        var viewModel = this.$get('product');
        var model = new Product(viewModel.description, viewModel.cost, viewModel.inventory)
        myPage.repository.add(model);
        myPage.router.go('/')
      }
    }
  }),
  vueDeleteProduct: Vue.extend({
    template: '#delete-product',
    data: function () {
      return {
        product: myPage.repository.find(this.$route.params.product_id)
      }
    },
    methods: {
      delete: function (e) {
        e.preventDefault();
        myPage.repository.delete(this.$route.params.product_id);
        myPage.router.go('/');
      }
    }
  }),
  router: new VueRouter(),
  initRouter: function () {
    myPage.router.map({
      '/': {
        component: myPage.vueProductList
      },
      '/add': {
        component: myPage.vueAddProduct
      },
      '/delete/:product_id': {
        component: myPage.vueDeleteProduct,
        name: 'delete-product'
      }
    })
      .start(Vue.extend({}), '#app');
  }
};

myPage.initRouter();