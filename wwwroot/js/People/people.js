PeoplePage = function (options) {
    var self = this;

    self.restClient = new restClient(options.apiUrl);

    self.router = new VueRouter();

    self.listView = Vue.extend({
        template: '#people-list',
        data: function () {
            return {
                people: []
            };
        },
        ready: function () {
            var listView = this;
            self.restClient.get('person', function (data) {
                listView.people = data;
            })
        }
    });

    self.router.map({
        '/': {
            component: self.listView
        }
    });
}

var peoplePage = new PeoplePage({
    apiUrl: 'http://' + window.location.host + '/api/'
});


peoplePage.router.start(Vue.extend({}), '#app')