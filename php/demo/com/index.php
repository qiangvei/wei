<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>com test</title>
</head>
<body>
<?php
require_once "WeiMssql.php";
?>
<?php
$obj = WeiMssql::getInstance();
$obj->setCode('GBK','UTF-8');
echo '<br />'.$obj->getVersion().'<br />';
$obj->connect('192.168.1.12','NP30','sa','cargpsmap');
$sql = "select top 5000 a.* 
from FB_FBA_DeliveryRecord as a 
left join WDGJ31.wdgj31.dbo.G_Cfg_WareHouseList as b on b.WarehouseID=a.Ffd_ToWarehouseID
where a.Ffd_Risk like '%评分%'";
//$sql = 'select top 5000 a.*
//from FB_FBA_DeliveryRecord as a
//left join WDGJ31.wdgj31.dbo.G_Cfg_WareHouseList as b on b.WarehouseID=a.Ffd_ToWarehouseID
//where a.Ffd_Risk like "%评分%"';
$rs = $obj->rs($sql);
//print_r($rs);
if(empty($rs)){ echo '无数据'.$rs;}
else if(is_array($rs)){
    foreach ($rs as $k=>$v){
        echo '<br /><br />'.$k.'<br />';
        print_r($v);
//        foreach ($v as $kk=>$vv){
//            echo $kk.'='.$obj::x2u(base64_decode($vv)).' , ';
//        }
    }
}else{
    print_r($rs);
}
echo '<br /><br />';
//print_r(WeiMssql::execute($sql));
echo '<br /><br />';
print_r($obj->getError());
?>
</body>
</html>