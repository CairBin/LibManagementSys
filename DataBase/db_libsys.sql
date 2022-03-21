/*
Navicat MySQL Data Transfer

Source Server         : db_libsys
Source Server Version : 50726
Source Host           : localhost:3306
Source Database       : db_libsys

Target Server Type    : MYSQL
Target Server Version : 50726
File Encoding         : 65001

Date: 2022-03-21 23:22:23
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `tb_admin`
-- ----------------------------
DROP TABLE IF EXISTS `tb_admin`;
CREATE TABLE `tb_admin` (
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL DEFAULT 'D4-DC-56-65-64-E9-42-18-8D-E6-25-E9-DE-52-1B-89',
  `id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=1002 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_admin
-- ----------------------------
INSERT INTO `tb_admin` VALUES ('admin', 'D4-DC-56-65-64-E9-42-18-8D-E6-25-E9-DE-52-1B-89', '1000');
INSERT INTO `tb_admin` VALUES ('admin2', 'D4-DC-56-65-64-E9-42-18-8D-E6-25-E9-DE-52-1B-89', '1001');

-- ----------------------------
-- Table structure for `tb_book`
-- ----------------------------
DROP TABLE IF EXISTS `tb_book`;
CREATE TABLE `tb_book` (
  `ISBN` varchar(255) NOT NULL,
  `bookname` varchar(255) NOT NULL,
  `author` varchar(255) NOT NULL,
  `publisher` varchar(255) NOT NULL,
  `bookclass` varchar(255) NOT NULL,
  `stock_num` int(10) unsigned NOT NULL DEFAULT '0',
  `total_num` int(10) unsigned NOT NULL DEFAULT '0',
  `price` double unsigned NOT NULL DEFAULT '0',
  `date` varchar(255) NOT NULL DEFAULT '0000-00-00',
  PRIMARY KEY (`ISBN`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_book
-- ----------------------------

-- ----------------------------
-- Table structure for `tb_borrowed_book`
-- ----------------------------
DROP TABLE IF EXISTS `tb_borrowed_book`;
CREATE TABLE `tb_borrowed_book` (
  `id` int(11) NOT NULL,
  `username` varchar(255) NOT NULL,
  `ISBN` varchar(255) NOT NULL,
  `bookname` varchar(255) NOT NULL,
  `author` varchar(255) NOT NULL,
  `publisher` varchar(255) NOT NULL,
  `bookclass` varchar(255) NOT NULL,
  `flag` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`flag`)
) ENGINE=MyISAM AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_borrowed_book
-- ----------------------------
INSERT INTO `tb_borrowed_book` VALUES ('10000', 'cairbin', '1', 'CairBin', 'Cair', 'Publisher', 'Class', '5');

-- ----------------------------
-- Table structure for `tb_user`
-- ----------------------------
DROP TABLE IF EXISTS `tb_user`;
CREATE TABLE `tb_user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL DEFAULT 'D4-DC-56-65-64-E9-42-18-8D-E6-25-E9-DE-52-1B-89',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=10006 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tb_user
-- ----------------------------
INSERT INTO `tb_user` VALUES ('10000', 'cairbin', 'D4-DC-56-65-64-E9-42-18-8D-E6-25-E9-DE-52-1B-89');
INSERT INTO `tb_user` VALUES ('10004', 'bob', 'D4-DC-56-65-64-E9-42-18-8D-E6-25-E9-DE-52-1B-89');
