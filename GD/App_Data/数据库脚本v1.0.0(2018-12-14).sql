/*
Navicat MySQL Data Transfer

Source Server         : 192.1.2.121
Source Server Version : 50720
Source Host           : localhost:3306
Source Database       : gd

Target Server Type    : MYSQL
Target Server Version : 50720
File Encoding         : 65001

Date: 2018-12-14 13:03:53
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for taudiencesinfo
-- ----------------------------
DROP TABLE IF EXISTS `taudiencesinfo`;
CREATE TABLE `taudiencesinfo` (
  `AI_Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '应用系统标识',
  `AI_Name` varchar(16) NOT NULL COMMENT '听众名称',
  `AI_PyName` varchar(16) NOT NULL COMMENT '拼音简写',
  `AI_Sex` varchar(2) NOT NULL COMMENT '性别',
  `AI_Age` varchar(8) NOT NULL COMMENT '年龄',
  `AI_Birth` varchar(32) NOT NULL COMMENT '生日',
  `AI_Tel` varchar(128) NOT NULL COMMENT '电话',
  `AI_CarCode` varchar(32) NOT NULL COMMENT '车牌',
  `AI_Certi` varchar(64) NOT NULL COMMENT '证件',
  `AI_Addr` varchar(256) NOT NULL COMMENT '地址',
  `AI_Occu` varchar(256) NOT NULL COMMENT '职业',
  `AI_Email` varchar(128) NOT NULL COMMENT '邮箱',
  `AI_FromTime` datetime NOT NULL COMMENT '最后来电时间',
  `AI_Remark` varchar(512) NOT NULL COMMENT '备注',
  `AI_Type` int(10) NOT NULL COMMENT 'ting种类型',
  PRIMARY KEY (`AI_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for tdatabaseinfo
-- ----------------------------
DROP TABLE IF EXISTS `tdatabaseinfo`;
CREATE TABLE `tdatabaseinfo` (
  `GuidID` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(100) DEFAULT NULL,
  `AnotherName` varchar(100) DEFAULT NULL,
  `DatabaseAddress` varchar(100) DEFAULT NULL,
  `DatabaseName` varchar(100) DEFAULT NULL,
  `DatabasePwd` varchar(100) DEFAULT NULL,
  `RecordAddress` varchar(100) DEFAULT NULL,
  `RecordUserName` varchar(100) DEFAULT NULL,
  `RecordPwd` varchar(100) DEFAULT NULL,
  `Isdel` tinyint(4) DEFAULT NULL,
  `Addtime` datetime DEFAULT NULL,
  `Updatetime` datetime DEFAULT NULL,
  PRIMARY KEY (`GuidID`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for trec
-- ----------------------------
DROP TABLE IF EXISTS `trec`;
CREATE TABLE `trec` (
  `PKID` int(11) NOT NULL AUTO_INCREMENT,
  `Calling` varchar(20) NOT NULL COMMENT '主叫号码',
  `Called` varchar(20) NOT NULL COMMENT '被叫号码',
  `StartTime` datetime NOT NULL COMMENT '开始时间',
  `EndTime` datetime NOT NULL COMMENT '结束时间',
  `FileLen` int(11) NOT NULL COMMENT '录音时长',
  `FilePath` varchar(255) NOT NULL COMMENT '录音存放位置',
  `FileName` varchar(128) NOT NULL COMMENT '录音文件名',
  `FileFormat` int(11) NOT NULL COMMENT '录音格式',
  `Memo` varchar(128) NOT NULL COMMENT '备注',
  PRIMARY KEY (`PKID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for ttel
-- ----------------------------
DROP TABLE IF EXISTS `ttel`;
CREATE TABLE `ttel` (
  `PKID` int(10) unsigned zerofill NOT NULL AUTO_INCREMENT COMMENT '记录标识',
  `Calling` varchar(255) NOT NULL COMMENT '主叫号码',
  `Called` varchar(255) NOT NULL COMMENT '被叫号码',
  `State` int(10) NOT NULL COMMENT '呼叫状态',
  `StartTime` datetime NOT NULL COMMENT '开始时间',
  `EndTime` datetime NOT NULL COMMENT '结束时间',
  `TalkingTime` int(11) NOT NULL COMMENT '通话时长',
  PRIMARY KEY (`PKID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for tuser
-- ----------------------------
DROP TABLE IF EXISTS `tuser`;
CREATE TABLE `tuser` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT COMMENT '用户标识',
  `UserName` varchar(32) NOT NULL COMMENT '用户名',
  `UserCode` varchar(16) NOT NULL COMMENT '用户编码',
  `UserPwd` varchar(32) NOT NULL COMMENT '用户密码',
  `UserType` int(10) DEFAULT NULL COMMENT '用户类型',
  `Telephone` varchar(20) NOT NULL COMMENT '用户联系电话',
  `Mobile` varchar(20) NOT NULL COMMENT '手机',
  `Birthday` datetime NOT NULL COMMENT '生日',
  `Address` varchar(128) NOT NULL COMMENT '地址',
  `LastTeleTime` datetime NOT NULL COMMENT '最近一次练习时间',
  `Memo` varchar(128) NOT NULL COMMENT '备注',
  `Email` varchar(20) NOT NULL COMMENT '邮箱',
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `UserID` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(100) DEFAULT NULL,
  `UserPwd` varchar(100) DEFAULT NULL,
  `Isdel` tinyint(4) DEFAULT NULL COMMENT '0未删除，1已删除',
  `Addtime` datetime DEFAULT NULL,
  `Updatetime` datetime DEFAULT NULL,
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
