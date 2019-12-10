<?php


class WeiMssql
{
    private static $_instance = null;
    private static $dbObj = null;
    private static $_errors = array();
    private static $_inCode = 'utf-8';
    private static $_outCode = 'utf-8';
    public function __construct()
    {
        try {
            self::$dbObj = new COM("Wei.Mssql");
        }catch (com_exception $e){
            self::$_errors[] = self::x2u($e->getMessage());
        }
    }
    private function __clone()
    {
        // TODO: Implement __clone() method.
    }
    public function setCode($inCode,$outCode){
        self::$_inCode = $inCode;
        self::$_outCode = $outCode;
    }
    public static function getInstance(){
        if(!(self::$_instance instanceof WeiMssql)){
            self::$_instance = new self();
        }
        return self::$_instance;
    }

    public function connect($server, $database, $uid, $pwd){
        try {
            self::getInstance();
            $flag = false;
            if(self::$dbObj){
                $flag = self::$dbObj->Con($server,$database,$uid,$pwd);
            }
            return $flag;
        }catch (com_exception $e){
            self::$_errors[] = self::x2u($e->getMessage());
            return false;
        }
    }

    public function execute($sql){
        try {
            $rs = -1;
            if(self::$dbObj){
                $rs = (int)self::$dbObj->SQL($sql);
            }
            return $rs<0?false:$rs;
        }catch (com_exception $e){
            self::$_errors[] = self::x2u($e->getMessage());
            return false;
        }
    }

    public function rs($sql){
        $sql = iconv('UTF-8','GB2312',$sql);
        try {
            $data = false;
            if(self::$dbObj){
                $data = self::$dbObj->RS($sql);
                $data = self::x2u($data);
                $data = json_decode($data);
                $data = self::rsDecode($data);
            }
            return $data;
        }catch (com_exception $e){
            self::$_errors[] = self::x2u($e->getMessage());
            return false;
        }
    }

    public static function rsDecode($data){
        $ret = [];
        if (!empty($data)){
            foreach ($data as $k=>$row){
                foreach ($row as $field=>$value){
                    $ret[$k][self::x2u($field)] = self::x2u(base64_decode($value));
                }
            }
        }
        return $ret;
    }

    public function getVersion(){
        try {
            $data = false;
            if(self::$dbObj){
                $data = self::$dbObj->Version();
            }
            return $data;
        }catch (com_exception $e){
            self::$_errors[] = self::x2u($e->getMessage());
            return false;
        }
    }

    public function getError(){
        try {
            $tmp = empty(self::$dbObj)?"":self::x2u(self::$dbObj->GetError());
            if(trim($tmp)!=''){
                self::$_errors[] = $tmp;
            }
        }catch (com_exception $e){
            self::$_errors[] = self::x2u($e->getMessage());
        }
        return array_filter(self::$_errors);
    }

    public static function x2x($str){
        if(strtolower(self::$_inCode)==strtolower(self::$_outCode)){
            return $str;
        }else{
            return iconv(self::$_inCode,self::$_outCode , $str);
        }
    }
    public static function x2u($str,$incode=''){
        $incode = empty($incode)?self::$_inCode:$incode;
        return strtolower($incode)=='utf-8'?$str:iconv($incode,"UTF-8" , $str);
    }
}