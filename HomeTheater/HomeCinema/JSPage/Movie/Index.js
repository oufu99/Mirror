var dataSource = {
    Search: { PageIndex: 10, PageSize: 10 },
    MovieList: []
}
var pageInfo = {
    pageIndex: 3,
    pageSize: 10,
    totalCount: 12
}
var vueMethods = {
    LoadMovies: function () {
        $.post("/Movie/LoadMovies", dataSource.Search, function (result) {
            dataSource.MovieList = result.data;
        }, "json")
    },
    ToDetail: function (id) {
        alert(id);

    },
    TestFun: function () {
        alert(this.message);
    }
}
var vuePageMethods = {
    SetPageHTML: function (pageModel) {
        //拼接html
        var html = "";
        html += '<a class="page" href="javascript: void (0)" onclick="vuePageMethods.GoToPage(1)">首页</a>';
        //遍历中间的10个标签出来
        if (pageModel.totalCount < 10) {
            for (var i = 1; i <= pageModel.totalCount; i++) {
                html += '<a class="page" href="javascript: void (0)" onclick="vuePageMethods.GoToPage(' + i + ')">' + i + '</a>';
            }
        }
        else {
            var beginIndex = pageModel.pageIndex;
            if ((pageModel.totalCount - pageModel.PageIndex) < pageModel.pageSize) {
                //如果总页数-当前页数 <10 的话,那么页面应该选中到最后而不应该继续加
                beginIndex = pageModel.totalCount - pageModel.PageIndex;
                for (; beginIndex < pageModel.totalCount; beginIndex++) {
                    //判断一下如果遍历的时候是这个页面,就加一个特别的class
                    html += '<a class="page" href="javascript: void (0)" onclick="vuePageMethods.GoToPage(' + i + ')">' + i + '</a>';
                }
            }
            else {
                //这里就是当前页面在中间就可以
                var beginIndex = pageModel.pageIndex - (pageModel.totalCount / 2);
                var endIndex = pageModel.pageIndex + (pageModel.totalCount / 2) - 1;
                for (; beginIndex < endIndex; beginIndex++) {
                    //判断一下如果遍历的时候是这个页面,就加一个特别的class
                    html += '<a class="page" href="javascript: void (0)" onclick="vuePageMethods.GoToPage(' + i + ')">' + i + '</a>';
                }
            }
        }
    },
    GoToPage: function (pageIndex) {
        pageInfo.pageIndex = pageIndex;
        vueMethods.LoadMovies();
    }

}
var vm = new Vue({
    el: '#app',
    data: dataSource,
    methods: vueMethods,
    mounted: function () {
        //加载电影列表
        this.LoadMovies();

    }
})
var vm2 = new Vue({
    el: '#footDiv',
    data: pageInfo,
    methods: vuePageMethods,
    mounted: function () {
    }
})