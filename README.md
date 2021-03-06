﻿# Word 记事本 v3.7
基于.NET的记事本风格的Word文档编辑器
## 基本介绍
记事本风格的Word文本编辑器，支持docx、txt编辑功能。支持修改字体字号，窗口置顶，自动保存、备份，暗黑模式，查找与替换，插入链接等高级功能。
## 免安装旗舰版本下载
请根据需要的功能选择以下两个之一（需要预装.NET framework 4.6及以上版本）：

[Word 记事本](https://github.com/IamWilliamWang/WordNotepad/releases/download/v3.7/WordNotepad.exe)

[日志书写器](https://github.com/IamWilliamWang/WordNotepad/releases/download/v3.7/TextWriter.exe)

## 稳定历史版本下载：

[Professional专业版](https://github.com/IamWilliamWang/WordNotepad/releases/download/v3.4/TextWriter.exe)(v3.4)

新增内容：精简文件大小。普通模式下添加状态栏，栏内显示行、列号、字数，还能控制是否锁定全屏模式、锁定滚动条开启状态。支持插入链接、字体下拉选择，Alt+上下挪动本行文字，Ctrl+Z撤销，Ctrl+Y重做。完善意外保护

[Community社区版](https://github.com/IamWilliamWang/WordNotepad/releases/download/v2.12/TextWriter.exe)(v2.12)

新增内容：添加了全屏模式、黑暗模式、自动聚焦、窗口置顶、自动保存、自动备份、自动恢复、文件拖动加载、保存终稿、插入中文空格、查找、快速查找、快捷输入、快捷删除以及出现各种意外情况的保护

[Lite精简版](https://github.com/IamWilliamWang/WordNotepad/releases/download/v1.6/DocxWriter.exe)(v1.6)

主要功能：基本的文本编辑，可以修改字体和字号，退出后可弹出邮箱，启动后自动加载内容

## 版本更新日志：
提示：加粗的版本号为稳定版本
|版本号|新功能|调整与修改|修复问题|
|:-:|:-|:-|:-|
|[**3.7**](https://github.com/IamWilliamWang/WordNotepad/releases/download/v3.7/WordNotepad.exe)|新增新建文档功能。增加未命名情况下关闭窗口处理。增加文本替换功能。增加Shift+Del删除并复制本行。支持拖拽至exe快速打开。Word记事本下支持恢复崩溃文档。替换文本框会猜测需要替换的文本。菜单内增加打开文档。全面支持加载与保存各种编码的txt文档(UTF-8、UTF-8带BOM、GBK、UTF-16LE、UTF-16BE、UTF7、UTF32)。支持选中文本统计字数。增加调试模式|Word记事本下：删除打开邮箱功能，不预加载日志书写器写的文件，高级设置不在未命名状态下显示，不检测后台运行。Ctrl+V会默记一次当前内容，新建文档时不会弹出邮箱。不允许弹出多个替换文本框。Ctrl+F继续搜索改为Ctrl+Alt+F。完善注释|修复Word记事本模式下：另存为docx后扔停在未命名状态问题，修复Ctrl+S引起保存功能崩溃问题。修复Shift+Del后删一字符的问题。修复查找内容、文本替换框被主窗口遮挡问题，修复列数为负的问题
|[3.6](https://github.com/IamWilliamWang/WordNotepad/releases/download/v3.6/WordNotepad.exe)|增加Ctrl+D删除本行、Ctrl+Alt+H隐藏任务栏显示|巨大的代码重构。日志书写器改版为Word记事本，模式可切换。恢复了自动暗黑模式|修复了计时器时长修改时产生了诸多问题。修复了加载新文档时旧文档保存到新路径的问题，修复了文件路径同步不选择一直弹出的问题
|3.5|添加了另存为功能，全屏模式下Ctrl+Alt+L可以锁定全屏模式|撤销、保存与跳过功能大幅修改，保证不会丢失当前的文档内容。跳过保存会显式的弹出提示。文件路径同步时会显示具体的路径|修复加载空文档未响应问题
|[**3.4**](https://github.com/IamWilliamWang/WordNotepad/releases/download/v3.4/TextWriter.exe)|添加强制保存功能。Ctrl+O可以载入非docx文件|不默认勾选退出后打开邮箱|修复了标题产生多余空格问题。修复了插入链接后界面混乱的问题
|3.3|增加了LF检测和LF转为CRLF功能。增加了Ctrl+Z撤销和Ctrl+Y重做功能|单击事件更新行列号。在回车、Ctrl+Up、Ctrl+Down、Ctrl+T、Ctrl+I、Ctrl+X、Ctrl+V时会保存为撤销点
|3.2|暗黑主题支持状态栏，Ctrl+I可以快捷插入超链接
|3.1|Alt+Up上移本行代码，Alt+Down下移本行代码。状态栏添加普通/精简模式锁定操作和更改滚动条锁定操作。选中文本后可插入链接|删除高级设置中“滚动条强制打开”，字体设置改为下拉框
|3.0|添加设置面板（备份时长设置，停用/启用自动备份/保存，清除备份文件，滚动条强制打开）。添加状态栏，动态显示行列号和字数。全屏模式时自动隐藏|压缩动态链接库文件
|[**2.12**](https://github.com/IamWilliamWang/WordNotepad/releases/download/v2.12/TextWriter.exe)|添加自动保存功能，菜单添加窗口置顶，增加字体修改保护|自动备份减少次数|越界报错，弹出查找框时被主窗口遮挡
|2.11|支持快捷插入"。支持删除左括号同时自动删除右括号（包含其他所有快捷插入的符号）。双击文件路径可以打开该文件夹|不自动开启黑暗模式
|2.10|Ctrl+F可继续搜索下一个关键词||搜索相同内容未响应的问题
|2.9||路径修改保护：保存前检测路径修改是否一致性。统一字体修改，说明文字略调|保存失败不会显示成功，快捷补全功能修复
|2.8|支持加载文本文档和加载异常检测(大小、格式)。支持加载字体。添加Ctrl T&Ctrl F键盘快捷方式
|2.7|支持读取字号。支持保存后置为终稿|拖入docx后会迁移操作目标文件、文件夹
|2.6|Backup支持多点后缀名与单次执行，自动备份可停/启用。左括号引号等支持自动补全
|2.5||内部代码重构，完全分离自动备份功能
|2.4|支持取消退出程序，拖拽加载文档|规范暗黑、全屏、聚焦模式状态显示
|2.3|支持自动聚焦窗口（选择开启），右键菜单支持取消暗黑模式
|2.2|添加右键菜单，支持查找、插入、剪切、复制、粘贴、删除、切换暗黑模式和全屏模式|界面混乱问题
|2.1|支持暗黑模式
|2.0|每半分钟自动备份，崩溃后可自动恢复。支持全选、滚轮、自动ScrollBars
|1.8|支持全屏模式
|1.7|支持窗口置顶、取消置顶
|[**1.6**](https://github.com/IamWilliamWang/WordNotepad/releases/download/v1.6/DocxWriter.exe)|||未保存内容检测的问题
|1.5|窗口显示版本号|窗口更大
|1.4|启动时自动加载保存的文件
|1.3|||换行储存为空格的问题
|1.2|退出后可以弹出邮箱|动态库内置
|1.1|字号可选，支持中文版本字号
|1.0|可以写日志，设置字体字号，保存日志，未保存关闭会提示