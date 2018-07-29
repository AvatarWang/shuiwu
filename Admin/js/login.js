var config = new Config();
var vm = new Vue({
	el: '#app',
	data: {
		userAccount: '',
		password: ''
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
                console.log(res);
                var data = res.data;
                if(data.Status==1){
                    console.log('登录成功');
                    window.location.replace("../index.html?userId="+data.UserId);
                };
			});		
		}
	}
});