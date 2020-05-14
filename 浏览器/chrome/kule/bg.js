//插件载入时，初始化配置
let kuleStatus =true
chrome.runtime.onInstalled.addListener(function() {
  //读取配置
    chrome.storage.local.get(['kuleStatus'],function(result){
      kuleStatus = result.kuleStatus;
      if(kuleStatus===undefined){
        //未设置时，进行初始化配置
        kuleStatus = true
        chrome.storage.local.set({kuleStatus:kuleStatus});
      }
      //在回调中设置扩展的状态
      toolUisetting();
  });
});
//修改扩展UI的方法
function toolUisetting(){      
      if(kuleStatus===true){
        chrome.browserAction.setIcon({path:'images/kule.png'});//默认是开启状态的图片，如果这里设置相同时会报错  
        chrome.browserAction.setBadgeText({text:'开启'});//显示开启文字
        chrome.browserAction.setBadgeBackgroundColor({color:"#15B835"});//文字背景
      }else{
        chrome.browserAction.setIcon({path:"images/kulehb.png"});  
        chrome.browserAction.setBadgeText({text:'关闭'});//显示开启文字
        chrome.browserAction.setBadgeBackgroundColor({color:"#CCCCCC"});//文字背景
      }
}

//监听扩展按钮被点击
chrome.browserAction.onClicked.addListener(function(){
  if(kuleStatus===true){
    kuleStatus = false;
  }else{
    kuleStatus = true;
  }
  chrome.storage.local.set({kuleStatus:kuleStatus});
  toolUisetting();
});

//监听存储值发生变化
chrome.storage.onChanged.addListener(function(changes,areaName){
    // console.log("存储值发生改变");
    // console.log(changes);
    // console.log(areaName);
});

//创建右键菜单
chrome.contextMenus.create({
  id:'kuletongapps',
  title: '酷乐应用', // %s表示选中的文字
  contexts: ['selection'], // 只有当选中文字时才会出现此右键菜单
  type:'normal'
});

//监听右键自定义菜单被点击
chrome.contextMenus.onClicked.addListener(function(object){
  console.log(object);
});



