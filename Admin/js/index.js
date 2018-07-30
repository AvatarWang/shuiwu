var config = new Config();
var params = config.parseQueryString(window.location.href);
var token = sessionStorage.getItem("token");
if (!token) {
    window.location.replace("../login.html");
};
var vm = new Vue({
    el: '#app',
    data: {
        userId: token,
        questionList: [],
        timer: [],
        isSubmit: '',
        score: '',
        isMobile: ''
    },
    created: function () {
        var userQuestionList = localStorage.getItem("userQuestionList_" + this.userId);
        if (userQuestionList) {
            this.$nextTick(function () {
                vm.questionList = JSON.parse(userQuestionList);
            });
        }
        this.$nextTick(function () {
            axios({
                method: 'post',
                url: config.getQuestionList_href,
                data: {
                    userid: this.userId
                }
            }).then(function (res) {
                if (res.data.Status == "1") {
                    if (!userQuestionList) {
                        vm.questionList = res.data.QuestionList;
                        for (i = 0; i < vm.questionList.length; i++) {
                            vm.questionList[i].name = 'question' + (i + 1);
                            vm.questionList[i].userAnswerList = [false, false, false, false];
                        };
                    };
                    vm.isSubmit = res.data.IsSubmit == '0' ? false : true;
                    vm.score = res.data.Score;
                    var finishTime = parseInt(res.data.FirstLoginTime) + 1800;
                    var currentTime = parseInt(res.data.NowTime);
                    var minute = Math.floor((finishTime - currentTime) / 60);
                    var second = (finishTime - currentTime) % 60;
                    var finish = false;
                    var timergo = function () {
                        setTimeout(function () {
                            second--;
                            if (second <= 0) {
                                second = 59;
                                minute--;
                            }
                            if (minute >= 0) {
                                timergo();
                            }
                            if (minute < 0) {
                                vm.isSubmit = true;
                                vm.saveAnswer();
                            }
                            vm.timer = [minute, second];
                        }, 1000);
                    };
                    if (!vm.isSubmit) {
                        timergo();
                    };
                };
            });
        });
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
        choseAnswer: function () {
            localStorage.removeItem("userQuestionList_" + this.userId);
            this.$nextTick(function () {
                localStorage.setItem("userQuestionList_" + this.userId, JSON.stringify(vm.questionList));
            });
        },
        saveAnswer: function () {
            var params = {
                UserId: this.userId,
                UserAnswerList: []
            };
            for (i = 0; i < vm.questionList.length; i++) {
                var answerArray = vm.questionList[i].userAnswerList.filter(function (item) {
                    return item != false;
                });
                var questionItem = {
                    QuestionId: vm.questionList[i].Id,
                    Answer: answerArray.join("")
                }
                params.UserAnswerList.push(questionItem);
            }

            axios({
                method: 'post',
                url: config.submitAnswer_href,
                data: params
            }).then(function (res) {
                if (res.data.Status == '1') {
                    vm.isSubmit = true;
                    vm.score = res.data.Score;
                }
            });
        }
    }
});
