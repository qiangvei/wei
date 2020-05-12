chrome.browserAction.onClicked.addListener(function(){
  console.log('点击了扩展栏按钮');
});

chrome.contextMenus.create({
  id:'kuletongapps',
  title: '酷乐应用', // %s表示选中的文字
  contexts: ['page'], // 只有当选中文字时才会出现此右键菜单
});

//监听自定义菜单被点击
chrome.contextMenus.onClicked.addListener(function(object){
  console.log(object);
});
//监听存储值发生变化
chrome.storage.onChanged.addListener(function(changes,areaName){
    console.log("存储值发生改变");
    console.log(changes);
    console.log(areaName);
});

let isRunning = false;

chrome.storage.local.set({isRunning: isRunning}, function() {
  console.log('Value is set');
});

chrome.storage.local.get(['isRunning'],function(result){
  console.log(result.isRunning);
});


