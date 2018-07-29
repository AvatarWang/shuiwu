function Config() {
    var test = false;
	if (test) {
		// 测试假数据
		this.login_href = '../js/data/login.json';
        this.getQuestionList_href = '../js/data/index.json';
	}else{
		// 正式环境
        this.login_href = '/Result/RemoteLogin';
        this.getQuestionList_href = '/Result/RemoteIndex';
	}
}
Config.prototype = {
    parseQueryString: function (url) {
        var params = {};
        url = decodeURI(url);
        var arr = url.split("?");
        if (arr.length <= 1)
            return params;
        arr = arr[1].split("&");
        for (var i = 0, l = arr.length; i < l; i++) {
            if (arr[i]) {
                var a = arr[i].split("=");
                if (a && a.length > 1) {
                    if (a[1].indexOf('#') >= 0) {
                        var hash = a[1].split("#");
                        params[a[0]] = hash[0];
                        params['hash'] = hash[1];
                    } else {
                        params[a[0]] = a[1];
                    }
                }

            }
        }
        return params;
    }
};
