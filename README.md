# LibManagementSys
LibManagementSys

## 介绍
图书管理系统

## 开发环境
* .net6.0
* MySQL 5.7.26

## 提示
在使用时，需手动导入DataBase文件夹下单sql数据库文件

## 功能介绍

* 登录功能
* 支持注册功能
* 支持密码哈希加密(SHA256,MD5)
* 支持连接数据库
* 图书查询功能
* 图书管理功能
* 用户管理功能
* 图书借阅功能
* 图书归还功能

## 数据库结构

* db_libsys
* * tb_user 普通账户表
* * tb_admin 管理员账户表
* * tb_book 图书表
* * tb_borrowed_book 借书表

## 项目目录

* LibManagementSys
* * AdminModel	管理员模块
* * BookInfo	图书信息
* * BookManage	图书管理模块
* * BookSearch	图书查询模块
* * BorrowBook	图书借还模块
* * BorrowBookInfo 所借图书信息
* * ChangePassword	密码更改模块
* * HashEncryption	哈希加密模块
* * LinkMysql	数据库操作模块
* * LoginModel	登录模块
* * MainMenu	主菜单模块
* * RegisterModel	注册模块
* * TableValue	表对应信息结构
* * UserInfo	用户信息
* * UserManage	用户管理模块
* * UserModel	用户模块
* * UserSearch	用户信息查询模块

![](https://raw.githubusercontent.com/CairBin/imgforblog/main/img/LibManagementSys.png)