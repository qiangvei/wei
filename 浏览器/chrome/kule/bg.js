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
//notifycation消息
function myNotifycation(){
  chrome.notifications.create("mynotifycations",{
    type:"progress", //basic , image , list , progress
    requireInteraction:true, //是否需要点击才关闭
    iconUrl:'images/kule.png',
    title:'请假审核提醒',
    message:'小江同学提交了一个请假单，事由：参加学习培训，请假日期：2020-05-15', //type:basic
    contextMessage:"人事系统消息",
    eventTime:Date.now() - 5000000,
    buttons:[{title:"不同意"},{title:'同意'}],
    //items:[{title:'item标题',message:'item消息'},{title:'item标题2',message:'item消息2'}], //消息加多行消息；type:list
    //imageUrl:'http://img.netbian.com/file/2019/1103/88a0c2293416705dd1df2a618169d9ba.jpg',//消息加图片；type:image
    progress: 50 ,//消息加进度条；type:progress
  })
}
//监听notifycation按钮的点击
chrome.notifications.onButtonClicked.addListener(function(notificationId,buttonIndex){
  console.log('点击了ID为：'+notificationId+' 的第 '+buttonIndex+' 个按钮');  
  chrome.notifications.clear(notificationId, function(wasCleared){
      console.log('是否关闭消息：'+wasCleared);
    })
});
//监听alarms触发
chrome.alarms.onAlarm.addListener(function(alarm){
  console.log(alarm);
  //定时器触发后，显示一个notifications
  myNotifycation();
});

//监听扩展按钮被点击
chrome.browserAction.onClicked.addListener(function(){
  if(kuleStatus===true){
    kuleStatus = false;
  }else{
    kuleStatus = true;
  }
  chrome.storage.local.set({kuleStatus:kuleStatus});
  toolUisetting();

  //弹窗消息权限是否开启
  chrome.notifications.getPermissionLevel(function(level){
      if(level=='granted'){
        myNotifycation();//显示一个消息框
      }else{
        console.log('消息弹窗权限为：'+level+' ，如果已经禁止，可以重新安装扩展来打开');        
      }
  });

  //1分钟后触发alarms事件，chrome.alarms.onAlarm.addListener(function callback)监听会触发
  chrome.alarms.create("test",{delayInMinutes:1});
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



