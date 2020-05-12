$(document).ready(function(){
    $('.primary').after(' <kat-button label="No" form-action="button" variant="primary" size="base"><button class="cancel style-scope kat-button" type="button">嵌入一个按钮</button></kat-button>');

    //触发页面元素响应事件的方法有两种：
    ////方法一：嵌入JS脚本，并在DOM元素上调用事件
    // $('head').append('<script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>');
    // $('head').append('<script src="http://192.168.1.7/insertpage.js"></script>');

    ////方法二：通过绑定元素事件。（推荐）
    $("button.cancel").click(function(){
        $("#orders-table>tbody>tr").each(function(index,data){
            let isChk = $(data).children('td').children('input').is(':checked');
            if(isChk){
                let odn = $(data).children('td').children('div.cell-body').children('div.cell-body-title').text();
                //window.Location.href = 'http://192.168.1.7/q123/index.php&od='+odn;
                window.open('http://192.168.1.7/q123/index.php?od='+odn);
            }
        });
    });

    chrome.storage.local.get(['isRunning'],function(result){
        console.log(result.isRunning);
      });
});

