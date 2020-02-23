$(function(){
//主导航
$('ul.nav li').hover(function(){
$(this).children('dl').slideDown();	
$(this).addClass('hover');
},function(){
$(this).children('dl').slideUp();	
$(this).removeClass('hover');	
});
//隔行变色
$('table.even_table tr:even').addClass('even_bj');
//最后一个li
$('ul.last_list').each(function() {
$(this).children('li:last').addClass('last');
});
//开始表单下拉框
/*
$('.search_select').each(function(index,domEle){
$(domEle).selectbox({
onChangeCallback: myFunction
});
});		
function myFunction(args){
alert(args.selectedVal);
}*/
});
