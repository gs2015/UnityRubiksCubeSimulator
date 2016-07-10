开发中遇到的问题及解决方法
==============
>### 同时旋转多块（一个面的所有块）
- 将面中的所有块放到一个父体中，然后旋转父物体
>### 设置父物体
- gameObject.transform.parent = target.transform
>### 为方块不同面上色
- 将一个方块分为6个GameObject，放到一个GameObject中 再分别上色
- gameObject.GetComponent<Renderer> ().material.color = Color.red;

>### 遍历GameObject中的子物体
-    int childCount = gameObject.transform.childCount;<br/>
     for (int i = 0; i < childCount; i++) <br/>
	 Transform child=gameObject.transform.GetChild (i);
>### 判断点到了方块的哪个面 
-  将鼠标点击事件放到面的脚本中去处理