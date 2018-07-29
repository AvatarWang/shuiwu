var config = new Config();
var params = config.parseQueryString(window.location.href);
var vm = new Vue({
	el: '#app',
	data: {
		userId: params['userId'],
        questionList: [],
        timer:[]
	},
	created: function(){
        var userQuestionList = localStorage.getItem("userQuestionList_"+this.userId);
        if(userQuestionList){
            this.$nextTick(function(){
                vm.questionList = JSON.parse(userQuestionList);
            });   
        }
		this.$nextTick(function(){
            axios({
                method: 'get',
                url: config.getQuestionList_href + "?userId=" + this.userId,
                data: {
                    userid: this.userId
                }
            }).then(function(res){
                if(res.data.Status==1){
                    if(!userQuestionList){
                        vm.questionList = res.data.QuestionList; 
                        for(i=0;i<vm.questionList.length;i++){
                            vm.questionList[i].name = 'question' + (i+1);
                            vm.questionList[i].userAnswerList = [false,false,false,false];
                        }; 
                    };
                    var finishTime = res.data.FirstLoginTime+1800;
                    var currentTime = res.data.NowTime;
                    var minute = Math.floor((finishTime-currentTime)/60);
                    var second = (finishTime - currentTime)%60;
                    var timergo = function(){
                        setTimeout(function(){
                            second--;
                            if(second==0){
                                second = 59;
                                minute--;
                            }
                            if(minute==0&&second==0){
                                return;
                            }
                            timergo();
                        },1000);
                        vm.timer = [minute,second];
                    };
                    timergo();
                };
            });
        }); 	
	},
	methods: {
		choseAnswer: function(){
            console.log(vm.questionList)
            localStorage.removeItem("userQuestionList_"+this.userId);
            this.$nextTick(function(){
                localStorage.setItem("userQuestionList_"+this.userId,JSON.stringify(vm.questionList));
            });
        }
	}
});