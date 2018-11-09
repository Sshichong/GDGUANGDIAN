# GDGUANGDIAN
一个.net菜鸡

## .net MVC 层次
#### Model 数据模型层  ==> java中的model
#### DAL   数据访问层  ==> java中的mapper
#### BLL   处理业务层  ==> java中的service


## 引用次序
#### ① 建Model
#### ② 建DAL，引用Model
#### ③ 建BLL，引用DAL，Model
#### ④ 项目，引用BLL，Model，不直接访问数据库



## 查询数据库
#### 查询数据库返回DataSet
#### 查询完先 dt.Table[0]
#### 条数=dt.Table[0].Rows.Count;
#### 遍历用foreach遍历，foreach (DataRow dr in dt.Rows){xxx}
