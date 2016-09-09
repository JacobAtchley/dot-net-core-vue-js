function restClient(rootUrl, bearerToken) {
    this.rootUrl = rootUrl;
    this.bearerToken = bearerToken;
}

restClient.prototype.ajax = function (controller, method, data, success, error, always) {
    var self = this;

    $.ajax({
        url: self.rootUrl + controller,
        type: method,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: data,
        headers: { Authorization: 'Bearer ' + this.bearerToken },
        success: success,
        error: function (result) {
            console.log(controller + ': ' + JSON.stringify(result));
            if (error) {
                error(result);
            }
        },
        complete: always,
        statusCode: {
            401: function () {
                location.reload(true);
            }
        }
    });
}

restClient.prototype.get = function (controller, success, error, always) {
    this.ajax(controller, 'GET', undefined, success, error, always);
};

restClient.prototype.getQuery = function (controller, data, success, error, always) {
    this.ajax(controller, 'GET', data, success, error, always);
};

restClient.prototype.post = function (controller, data, success, error, always) {
    this.ajax(controller, 'POST', data ? JSON.stringify(data) : undefined, success, error, always);
};

restClient.prototype.put = function (controller, data, success, error, always) {
    this.ajax(controller, 'PUT', data ? JSON.stringify(data) : undefined, success, error, always);
};