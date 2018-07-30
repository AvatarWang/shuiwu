var config = new Config();
var vm = new Vue({
	el: '#app',
	data: {
		userAccount: '',
        password: '',
        isMobile: ''
    },
    mounted: function () {
        var ua = navigator.userAgent;
        var ipad = ua.match(/(iPad).*OS\s([\d_]+)/),
            isIphone = !ipad && ua.match(/(iPhone\sOS)\s([\d_]+)/),
            isAndroid = ua.match(/(Android)\s+([\d.]+)/),
            isMobile = ipad || isIphone || isAndroid;
        if (isMobile) {
            this.isMobile = 'mobile';
        } 
    },
	methods: {
		toLogin: function(){
			axios({
			    method: 'get',
                url: config.login_href + "?account=" + this.userAccount + "&passWord=" + this.password,
			    data: {
			        username: this.userAccount,
			        password: this.password
			    }
			}).then(function(res){
                var data = res.data;
                if(data.Status=="1"){
                    console.log('登录成功');
                    sessionStorage.setItem("token",data.UserId);
                    window.location.replace("../index.html?userId="+data.UserId);
                };
			});		
		}
	}
});