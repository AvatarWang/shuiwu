﻿/*
kkpager V1.3
https://github.com/pgkk/kkpager
http://blog.csdn.net/gaoshanliushui2009/article/details/37736751

Copyright (c) 2013 cqzhangkang@163.com
Licensed under the GNU GENERAL PUBLIC LICENSE
*/
var kkpager = {
    pagerid: 'kkpager',
    mode: 'link',
    pno: 1,
    total: 1,
    totalRecords: 0,
    isShowFirstPageBtn: true,
    isShowLastPageBtn: true,
    isShowPrePageBtn: true,
    isShowNextPageBtn: true,
    isShowTotalPage: true,
    isShowCurrPage: true,
    isShowTotalRecords: false,
    isGoPage: true,
    isWrapedPageBtns: true,
    isWrapedInfoTextAndGoPageBtn: true,
    hrefFormer: '',
    hrefLatter: '',
    gopageWrapId: 'kkpager_gopage_wrap',
    gopageButtonId: 'kkpager_btn_go',
    gopageTextboxId: 'kkpager_btn_go_input',
    lang: {
        firstPageText: '首页',
        firstPageTipText: '首页',
        lastPageText: '尾页',
        lastPageTipText: '尾页',
        prePageText: '上一页',
        prePageTipText: '上一页',
        nextPageText: '下一页',
        nextPageTipText: '下一页',
        totalPageBeforeText: '共',
        totalPageAfterText: '页',
        currPageBeforeText: '当前第',
        currPageAfterText: '页',
        totalInfoSplitStr: '/',
        totalRecordsBeforeText: '共',
        totalRecordsAfterText: '条数据',
        totalRecordsInfoSplitStr: ',',
        gopageBeforeText: '&nbsp;转到',
        gopageButtonOkText: '确定',
        gopageAfterText: '页',
        buttonTipBeforeText: '第',
        buttonTipAfterText: '页'
    },
    getLink: function (n) {
        if (n == 1) {
            return this.hrefFormer + this.hrefLatter
        }
        return this.hrefFormer + '_' + n + this.hrefLatter
    },
    click: function (n) {
        return false
    },
    getHref: function (n) {
        return '#'
    },
    focus_gopage: function () {
        var btnGo = $('#' + this.gopageButtonId);
        $('#' + this.gopageTextboxId).attr('hideFocus', true);
        btnGo.show();
        btnGo.css('left', '10px');
        $('#' + this.gopageTextboxId).addClass('focus');
        btnGo.animate({
            left: '+=30'
        }, 50)
    },
    blur_gopage: function () {
        var _this = this;
        setTimeout(function () {
            var btnGo = $('#' + _this.gopageButtonId);
            btnGo.animate({
                left: '-=25'
            }, 100, function () {
                btnGo.hide();
                $('#' + _this.gopageTextboxId).removeClass('focus')
            })
        }, 400)
    },
    keypress_gopage: function () {
        var event = arguments[0] || window.event;
        var code = event.keyCode || event.charCode;
        if (code == 8) return true;
        if (code == 13) {
            kkpager.gopage();
            return false
        }
        if (event.ctrlKey && (code == 99 || code == 118)) return true;
        if (code < 48 || code > 57) return false;
        return true
    },
    gopage: function () {
        var str_page = $('#' + this.gopageTextboxId).val();
        if (isNaN(str_page)) {
            $('#' + this.gopageTextboxId).val(this.next);
            return
        }
        var n = parseInt(str_page);
        if (n < 1) n = 1;
        if (n > this.total) n = this.total;
        if (this.mode == 'click') {
            this._clickHandler(n)
        } else {
            window.location = this.getLink(n)
        }
    },
    selectPage: function (n) {
        this._config['pno'] = n;
        console.log("selectPage:" + this._config);
        this.generPageHtml(this._config, true)
    },
    generPageHtml: function (config, enforceInit) {
        if (enforceInit || !this.inited) {
            this.init(config)
        }
        var str_first = '',
            str_prv = '',
            str_next = '',
            str_last = '';
        if (this.isShowFirstPageBtn) {
            if (this.hasPrv) {
                str_first = '<a ' + this._getHandlerStr(1) + ' title="' + (this.lang.firstPageTipText || this.lang.firstPageText) + '">' + this.lang.firstPageText + '</a>'
            } else {
                str_first = '<span class="disabled">' + this.lang.firstPageText + '</span>'
            }
        }
        if (this.isShowPrePageBtn) {
            if (this.hasPrv) {
                str_prv = '<a ' + this._getHandlerStr(this.prv) + ' title="' + (this.lang.prePageTipText || this.lang.prePageText) + '">' + this.lang.prePageText + '</a>'
            } else {
                str_prv = '<span class="disabled">' + this.lang.prePageText + '</span>'
            }
        }
        if (this.isShowNextPageBtn) {
            if (this.hasNext) {
                str_next = '<a ' + this._getHandlerStr(this.next) + ' title="' + (this.lang.nextPageTipText || this.lang.nextPageText) + '">' + this.lang.nextPageText + '</a>'
            } else {
                str_next = '<span class="disabled">' + this.lang.nextPageText + '</span>'
            }
        }
        if (this.isShowLastPageBtn) {
            if (this.hasNext) {
                str_last = '<a ' + this._getHandlerStr(this.total) + ' title="' + (this.lang.lastPageTipText || this.lang.lastPageText) + '">' + this.lang.lastPageText + '</a>'
            } else {
                str_last = '<span class="disabled">' + this.lang.lastPageText + '</span>'
            }
        }
        var str = '';
        var dot = '<span class="spanDot">...</span>';
        var total_info = '<span class="totalText">';
        var total_info_splitstr = '<span class="totalInfoSplitStr">' + this.lang.totalInfoSplitStr + '</span>';
        if (this.isShowCurrPage) {
            total_info += this.lang.currPageBeforeText + '<span class="currPageNum">' + this.pno + '</span>' + this.lang.currPageAfterText;
            if (this.isShowTotalPage) {
                total_info += total_info_splitstr;
                total_info += this.lang.totalPageBeforeText + '<span class="totalPageNum">' + this.total + '</span>' + this.lang.totalPageAfterText;
                total_info += '<span class="totalRecordsInfoSplitStr">' + this.lang.totalRecordsInfoSplitStr + '</span>' + this.lang.totalRecordsBeforeText + '<span class="totalRecordNum">' + this.totalRecords + '</span>' + this.lang.totalRecordsAfterText
            } else if (this.isShowTotalRecords) {
                total_info += total_info_splitstr;
                total_info += this.lang.totalRecordsBeforeText + '<span class="totalRecordNum">' + this.totalRecords + '</span>' + this.lang.totalRecordsAfterText
            }
        } else if (this.isShowTotalPage) {
            total_info += this.lang.totalPageBeforeText + '<span class="totalPageNum">' + this.total + '</span>' + this.lang.totalPageAfterText;
            if (this.isShowTotalRecords) {
                total_info += total_info_splitstr;
                total_info += this.lang.totalRecordsBeforeText + '<span class="totalRecordNum">' + this.totalRecords + '</span>' + this.lang.totalRecordsAfterText
            }
        } else if (this.isShowTotalRecords) {
            total_info += this.lang.totalRecordsBeforeText + '<span class="totalRecordNum">' + this.totalRecords + '</span>' + this.lang.totalRecordsAfterText
        }
        total_info += '</span>';
        var gopage_info = '';
        if (this.isGoPage) {
            gopage_info = '<span class="goPageBox">' + this.lang.gopageBeforeText + '<span id="' + this.gopageWrapId + '">' + '<input type="button" id="' + this.gopageButtonId + '" onclick="kkpager.gopage()" value="' + this.lang.gopageButtonOkText + '" />' + '<input type="text" id="' + this.gopageTextboxId + '" onfocus="kkpager.focus_gopage()"  onkeypress="return kkpager.keypress_gopage(event);"   onblur="kkpager.blur_gopage()" value="' + this.next + '" /></span>' + this.lang.gopageAfterText + '</span>'
        }
        if (this.total <= 8) {
            for (var i = 1; i <= this.total; i++) {
                if (this.pno == i) {
                    str += '<span class="curr">' + i + '</span>'
                } else {
                    str += '<a ' + this._getHandlerStr(i) + ' title="' + this.lang.buttonTipBeforeText + i + this.lang.buttonTipAfterText + '">' + i + '</a>'
                }
            }
        } else {
            if (this.pno <= 5) {
                for (var i = 1; i <= 7; i++) {
                    if (this.pno == i) {
                        str += '<span class="curr">' + i + '</span>'
                    } else {
                        str += '<a ' + this._getHandlerStr(i) + ' title="' + this.lang.buttonTipBeforeText + i + this.lang.buttonTipAfterText + '">' + i + '</a>'
                    }
                }
                str += dot
            } else {
                str += '<a ' + this._getHandlerStr(1) + ' title="' + this.lang.buttonTipBeforeText + '1' + this.lang.buttonTipAfterText + '">1</a>';
                str += '<a ' + this._getHandlerStr(2) + ' title="' + this.lang.buttonTipBeforeText + '2' + this.lang.buttonTipAfterText + '">2</a>';
                str += dot;
                var begin = this.pno - 2;
                var end = this.pno + 2;
                if (end > this.total) {
                    end = this.total;
                    begin = end - 4;
                    if (this.pno - begin < 2) {
                        begin = begin - 1
                    }
                } else if (end + 1 == this.total) {
                    end = this.total
                }
                for (var i = begin; i <= end; i++) {
                    if (this.pno == i) {
                        str += '<span class="curr">' + i + '</span>'
                    } else {
                        str += '<a ' + this._getHandlerStr(i) + ' title="' + this.lang.buttonTipBeforeText + i + this.lang.buttonTipAfterText + '">' + i + '</a>'
                    }
                }
                if (end != this.total) {
                    str += dot
                }
            }
        }
        var pagerHtml = '<div>';
        if (this.isWrapedPageBtns) {
            pagerHtml += '<span class="pageBtnWrap">' + str_first + str_prv + str + str_next + str_last + '</span>'
        } else {
            pagerHtml += str_first + str_prv + str + str_next + str_last
        } if (this.isWrapedInfoTextAndGoPageBtn) {
            pagerHtml += '<span class="infoTextAndGoPageBtnWrap">' + total_info + gopage_info + '</span>'
        } else {
            pagerHtml += total_info + gopage_info
        }
        pagerHtml += '</div><div style="clear:both;"></div>';
        $("#" + this.pagerid).html(pagerHtml)
    },
    init: function (config) {
        this.pno = isNaN(config.pno) ? 1 : parseInt(config.pno);
        this.total = isNaN(config.total) ? 1 : parseInt(config.total);
        this.totalRecords = isNaN(config.totalRecords) ? 0 : parseInt(config.totalRecords);
        if (config.pagerid) {
            this.pagerid = config.pagerid
        }
        if (config.mode) {
            this.mode = config.mode
        }
        if (config.gopageWrapId) {
            this.gopageWrapId = config.gopageWrapId
        }
        if (config.gopageButtonId) {
            this.gopageButtonId = config.gopageButtonId
        }
        if (config.gopageTextboxId) {
            this.gopageTextboxId = config.gopageTextboxId
        }
        if (config.isShowFirstPageBtn != undefined) {
            this.isShowFirstPageBtn = config.isShowFirstPageBtn
        }
        if (config.isShowLastPageBtn != undefined) {
            this.isShowLastPageBtn = config.isShowLastPageBtn
        }
        if (config.isShowPrePageBtn != undefined) {
            this.isShowPrePageBtn = config.isShowPrePageBtn
        }
        if (config.isShowNextPageBtn != undefined) {
            this.isShowNextPageBtn = config.isShowNextPageBtn
        }
        if (config.isShowTotalPage != undefined) {
            this.isShowTotalPage = config.isShowTotalPage
        }
        if (config.isShowCurrPage != undefined) {
            this.isShowCurrPage = config.isShowCurrPage
        }
        if (config.isShowTotalRecords != undefined) {
            this.isShowTotalRecords = config.isShowTotalRecords
        }
        if (config.isWrapedPageBtns) {
            this.isWrapedPageBtns = config.isWrapedPageBtns
        }
        if (config.isWrapedInfoTextAndGoPageBtn) {
            this.isWrapedInfoTextAndGoPageBtn = config.isWrapedInfoTextAndGoPageBtn
        }
        if (config.isGoPage != undefined) {
            this.isGoPage = config.isGoPage
        }
        if (config.lang) {
            for (var key in config.lang) {
                this.lang[key] = config.lang[key]
            }
        }
        this.hrefFormer = config.hrefFormer || '';
        this.hrefLatter = config.hrefLatter || '';
        if (config.getLink && typeof (config.getLink) == 'function') {
            this.getLink = config.getLink
        }
        if (config.click && typeof (config.click) == 'function') {
            this.click = config.click
        }
        if (config.getHref && typeof (config.getHref) == 'function') {
            this.getHref = config.getHref
        }
        if (!this._config) {
            this._config = config
        }
        if (this.pno < 1) this.pno = 1;
        this.total = (this.total <= 1) ? 1 : this.total;
        if (this.pno > this.total) this.pno = this.total;
        this.prv = (this.pno <= 2) ? 1 : (this.pno - 1);
        this.next = (this.pno >= this.total - 1) ? this.total : (this.pno + 1);
        this.hasPrv = (this.pno > 1);
        this.hasNext = (this.pno < this.total);
        this.inited = true
    },
    _getHandlerStr: function (n) {
        if (this.mode == 'click') {
            return 'href="' + this.getHref(n) + '" onclick="return kkpager._clickHandler(' + n + ')"'
        }
        return 'href="' + this.getLink(n) + '"'
    },
    _clickHandler: function (n) {
        var res = false;
        if (this.click && typeof this.click == 'function') {
            res = this.click.call(this, n) || false
        }
        return res
    }
};