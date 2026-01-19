# 端阳悟理 (Duan Yang Wu Li)
<!-- 这是一张图片，ocr 内容为： -->
![](https://img.shields.io/badge/Unity-2022.3.17f1c1-000000?style=for-the-badge&logo=unity&logoColor=white)<!-- 这是一张图片，ocr 内容为： -->
![](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)<!-- 这是一张图片，ocr 内容为： -->
![](https://img.shields.io/badge/Baidu%20AI-ERNIE--4.0--Turbo-2932E1?style=for-the-badge&logo=baidu&logoColor=white)**将中国古代物理知识与传统端午节习俗相结合的教育游戏**[项目简介](#-项目简介) • [核心特色](#-核心特色) • [安装配置](#-安装与配置) • [技术架构](#-技术架构) • [游戏玩法](#-游戏玩法)

---

## 📖 项目简介
**端阳悟理** 是一款基于 Unity 3D 开发的虚拟现实教育游戏，巧妙地将**中国古代物理知识**（《墨经》、《天工开物》等）与**端午节传统习俗**相结合。玩家在筹备端午祭祀的过程中，通过游戏化叙事习得物理原理，体验传统文化与科学知识的深度融合。

### 🎯 核心理念
+ **寓教于乐**：通过游戏化机制让物理知识学习变得生动有趣
+ **文化传承**：融合端午节传统习俗，弘扬中华优秀传统文化
+ **科学启蒙**：以中国古代物理智慧为切入点，培养科学思维
+ **AI 赋能**：集成百度文心大模型，实现智能对话与动态出题

---

## ✨ 核心特色
### 🤖 AI 动态对话系统
+ **智能 NPC 对话**：与古风学者"安娘子"进行自然对话，支持**语音输入**和**文本输入**
+ **动态猜诗系统**：AI 实时生成融合物理线索与诗句线索的谜题，每次游戏体验不同
+ **Prompt 工程优化**：通过精心设计的系统提示词，确保 AI 输出符合古风语境与教育目标

### ⚙️ 物理引擎深度集成
+ **光的折射模拟**：捕鱼关卡中，玩家需瞄准鱼影下方才能击中实物，真实模拟视深与实深差异
+ **抛物线运动**：射箭关卡受重力和动态风力影响，需根据风速/风向预判轨迹
+ **热力学模拟**：烹饪关卡通过长按控制进度条，模拟火候与热传递过程
+ **结构力学**：鲁班锁拼装解谜，利用榫卯结构原理移除障碍

### 🎮 动态游戏机制
+ **动态迷宫生成**：基于网格扫描算法，在可行走区域内随机生成收集物
+ **多模态交互**：支持键盘、语音输入
+ **分层导航系统**：使用 NavMesh 分层烘焙，实现复杂地形的智能寻路

---

## 🚀 安装与配置
### 环境要求
+ **Unity 版本**：2022.3.17f1c1 或更高
+ **操作系统**：Windows 10/11, macOS, Linux
+ **开发工具**：Visual Studio 2022 
+ **.NET 版本**：.NET Framework 4.8 或更高

### 项目依赖
项目使用以下核心包（已包含在 `Packages/manifest.json`）：

+ `com.unity.ai.navigation` (1.1.5) - AI 导航系统
+ `com.unity.nuget.newtonsoft-json` (3.2.1) - JSON 解析
+ `com.unity.render-pipelines.universal` (14.0.9) - URP 渲染管线
+ `com.unity.textmeshpro` (3.0.6) - 文本渲染

### 📦 字体文件下载

> ⚠️ **重要提示**：由于字体文件体积较大，项目中的字体文件未包含在 Git 仓库中。请按照以下步骤下载并放置字体文件，以确保游戏正常显示。

#### 所需字体文件列表

项目需要以下字体文件，请下载后放置到对应目录：

| 字体名称 | 文件路径 | 用途 |
|---------|---------|------|
| **LXGW WenKai Mono GB Bold** | `Assets/Fonts/LXGWWenKaiMonoGB-Bold.ttf` | 等宽中文字体 |
| **Ma Shan Zheng Regular** | `Assets/TextMesh Pro/Fonts/Ma Shan Zheng Regular.ttf` | 古风手写字体 |
| **山海极古明刻（雅致版）** | `Assets/font/ShanHaiJiGuMingKe(YaZhiBan)-2.ttf` | 古风标题字体 |
| **丁列篆海字体** | `Assets/font/dingliezhuhaifont.ttf` | 篆书风格字体 |
| **Liberation Sans** | `Assets/TerrainDemoScene_URP/TextMeshPro/Fonts/LiberationSans.ttf` | 西文无衬线字体 |
| **Inter Regular** | `Assets/TerrainDemoScene_URP/TextMeshPro/Fonts/InterRegular.otf` | 现代西文字体 |
| **Inter Bold** | `Assets/TerrainDemoScene_URP/TextMeshPro/Fonts/InterBold.otf` | 现代西文字体（粗体） |

#### 下载方式

**方式一：从字体源网站下载（推荐）**

1. **LXGW WenKai Mono（霞鹜文楷等宽）**
   - 下载地址：[GitHub Releases](https://github.com/lxgw/LxgwWenKai-Mono/releases)
   - 选择 `LXGWWenKaiMonoGB-Bold.ttf` 下载

2. **Ma Shan Zheng（马善政手写体）**
   - 下载地址：[Google Fonts](https://fonts.google.com/specimen/Ma+Shan+Zheng)
   - 或搜索 "Ma Shan Zheng" 字体下载

3. **Inter 字体**
   - 下载地址：[GitHub Releases](https://github.com/rsms/inter/releases)
   - 选择 `Inter-Regular.otf` 和 `Inter-Bold.otf` 下载

4. **Liberation Sans**
   - 下载地址：[Liberation Fonts](https://github.com/liberationfonts/liberation-fonts/releases)
   - 或通过系统字体库获取

5. **山海极古明刻、丁列篆海字体**
   - 这些为特殊古风字体，请从原始字体供应商或字体分享平台获取
   - 如无法获取，可使用其他古风字体替代

**方式二：使用备用字体（快速开始）**

如果无法获取上述字体，可以使用 Unity 默认字体或系统字体临时替代：

1. 打开 Unity 编辑器
2. 在 `Project` 窗口中找到使用字体的 Text 组件
3. 将字体替换为 Unity 默认字体 `Arial` 或系统字体
4. 注意：使用备用字体可能影响游戏视觉效果

#### 安装步骤

1. **下载字体文件**：按照上述方式下载所需字体文件

2. **创建目录结构**（如果不存在）：
   ```bash
   Assets/
   ├── Fonts/
   ├── font/
   ├── TextMesh Pro/
   │   └── Fonts/
   └── TerrainDemoScene_URP/
       └── TextMeshPro/
           └── Fonts/
   ```

3. **放置字体文件**：将下载的字体文件复制到对应的目录中

4. **刷新 Unity 项目**：
   - 在 Unity 编辑器中，右键点击 `Assets` 文件夹
   - 选择 `Refresh` 或按 `Ctrl+R`（Windows）/ `Cmd+R`（macOS）

5. **验证安装**：
   - 打开游戏场景，检查文本显示是否正常
   - 如果字体缺失，Unity 会在 Console 中显示警告

> 💡 **提示**：字体文件的 `.meta` 文件会在首次导入时自动生成，无需手动创建。

### 📝 百度 API 配置

> ⚠️ **重要提示**：项目中的敏感信息（API Key、Secret Key、Client ID、Client Secret、App ID）已用星号（`*`）替换，以保护隐私安全。在使用项目前，请务必按照以下步骤配置您自己的 API 密钥。

#### 1. 获取百度智能云 API 密钥
1. 访问 [百度智能云控制台](https://console.bce.baidu.com/)
2. 创建应用并获取以下凭证：
    - **语音识别服务**：API Key 和 Secret Key
    - **文心大模型服务**：Client ID 和 Client Secret

#### 2. 配置语音识别 API
编辑 `Assets/Script/AInpc/BaiduSpeechTranscribe.cs`，修改以下常量（第 43-44 行）：

```csharp
// 将星号替换为您的实际密钥
const string API_KEY = "YOUR_SPEECH_API_KEY";
const string SECRET_KEY = "YOUR_SPEECH_SECRET_KEY";
```

**注意**：代码中第 53 行还需要修改应用 ID（当前为星号占位符）：

```csharp
// 将星号替换为您的实际应用 ID
aipClient = new Asr("YOUR_APP_ID", API_KEY, SECRET_KEY);
```

#### 3. 配置文心大模型 API
编辑以下文件，修改文心大模型的凭证（将星号替换为您的实际密钥）：

**文件 1**：`Assets/Script/AInpc/BaiduTranslate.cs`（第 62-63 行）

```csharp
// 将星号替换为您的实际密钥
private string client_id = "YOUR_ERNIE_CLIENT_ID";
private string client_secret = "YOUR_ERNIE_CLIENT_SECRET";
```

**文件 2**：`Assets/Script/Poetry/poetryTalk.cs`（第 51-52 行）

```csharp
// 将星号替换为您的实际密钥
private string client_id = "YOUR_ERNIE_CLIENT_ID";
private string client_secret = "YOUR_ERNIE_CLIENT_SECRET";
```

#### 4. 配置 API 端点（可选）
默认使用以下端点，如需修改请编辑对应脚本：

+ **文心大模型**：`https://aip.baidubce.com/rpc/2.0/ai_custom/v1/wenxinworkshop/chat/ernie-4.0-turbo-128k`
+ **Token 获取**：`https://aip.baidubce.com/oauth/2.0/token`

#### 5. 验证配置
1. 打开 Unity 编辑器
2. 运行场景，测试语音识别和 AI 对话功能
3. 查看 Console 日志，确认 API 调用成功

> ⚠️ **安全提示**：请勿将包含真实 API Key 的代码提交到公共仓库。建议使用 Unity 的 `ScriptableObject` 或环境变量管理敏感信息。
>

---

## 🏗️ 技术架构
### 系统架构图
```plain
┌─────────────────────────────────────────────────────────┐
│                     Unity 游戏引擎                        │
├─────────────────────────────────────────────────────────┤
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐  │
│  │  物理引擎    │  │  AI 导航     │  │  渲染管线    │  │
│  │  (PhysX)     │  │  (NavMesh)   │  │  (URP)       │  │
│  └──────────────┘  └──────────────┘  └──────────────┘  │
└─────────────────────────────────────────────────────────┘
                          │
                          ▼
┌─────────────────────────────────────────────────────────┐
│                  AI 对话系统通信流程                      │
├─────────────────────────────────────────────────────────┤
│                                                           │
│  玩家输入（语音/文本）                                     │
│         │                                                 │
│         ▼                                                 │
│  ┌──────────────────┐                                    │
│  │ 语音识别模块      │                                    │
│  │ BaiduSpeech      │                                    │
│  │ Transcribe       │                                    │
│  └────────┬─────────┘                                    │
│           │                                               │
│           ▼                                               │
│  ┌──────────────────┐                                    │
│  │ 动态 Prompt 引擎 │                                    │
│  │ (加载 Prompt.txt)│                                    │
│  └────────┬─────────┘                                    │
│           │                                               │
│           ▼                                               │
│  ┌──────────────────┐                                    │
│  │ 文心大模型 API   │                                    │
│  │ (ERNIE-4.0)      │                                    │
│  └────────┬─────────┘                                    │
│           │                                               │
│           ▼                                               │
│  ┌──────────────────┐                                    │
│  │ 响应解析模块      │                                    │
│  │ (正则表达式提取)   │                                    │
│  └────────┬─────────┘                                    │
│           │                                               │
│           ▼                                               │
│  游戏反馈（UI 更新/状态变更）                              │
│                                                           │
└─────────────────────────────────────────────────────────┘
```

### 核心模块说明
#### 1. AI 对话系统 (`Assets/Script/AInpc/`)
+ **BaiduSpeechTranscribe.cs**：语音识别模块
    - 使用百度语音识别 SDK (`Baidu.Aip.Speech`)
    - 支持实时录音与转写
    - 异步处理，不阻塞主线程
+ **BaiduTranslate.cs**：通用 AI 对话模块
    - 管理对话历史记录
    - 动态构建 Prompt
    - 使用协程处理 HTTP 请求
+ **poetryTalk.cs**：猜诗系统核心
    - 解析 AI 返回的 JSON 数据
    - 提取物理线索和诗句线索
    - 管理游戏状态（出题/答题/解析）

#### 2. 物理模拟系统
+ **捕鱼关卡**：通过代码逻辑判定点击位置与实际 Collider 位置的偏移，模拟光的折射
+ **射箭关卡**：随机生成风力向量，实时作用于箭矢的 `Rigidbody`
+ **烹饪关卡**：通过进度条控制模拟热传递过程

#### 3. 动态迷宫生成 (`Assets/Script/MiGong/`)
+ **FarmGameManager.cs**：基于 `Vector3` 和 `CheckSphere` 扫描地图
+ 生成可行走区域列表 `walkablePositions`
+ 使用 `HashSet` 确保坐标不重复

### 数据流示例
**猜诗系统完整流程**：

```plain
1. 玩家点击"开始游戏"
   ↓
2. poetryTalk.cs 构建请求体（包含系统 Prompt）
   ↓
3. 发送 HTTP POST 请求到文心大模型 API
   ↓
4. AI 返回包含双线索提示和选项的 JSON
   ↓
5. 使用正则表达式提取：
   - 物理线索 (physics_clue)
   - 诗句线索 (verse_clue)
   - 四个选项 (A/B/C/D)
   - 正确答案
   ↓
6. 更新 UI 显示
   ↓
7. 玩家提问/猜测 → 循环步骤 2-6
```

---

## 🎮 游戏玩法
### 关卡总览
| 关卡名称 | 端午习俗 | 物理知识点 | 核心机制 |
| --- | --- | --- | --- |
| **1. 捕鱼备鲜** | 鱼叉捕鱼 | 光的折射 | 瞄准鱼影下方击中实物 |
| **2. 稻田巧径** | 收割糯米 | 结构力学 | 迷宫探索 + 鲁班锁拼装 |
| **3. 鱼米炊烟** | 包粽子/烹饪 | 热力学 | 长按控制火候进度条 |
| **4. 妙射五毒** | 射五毒 | 抛物线运动 & 力的合成 | 根据风速/风向预判轨迹 |
| **5. 猜诗探锦** | 对诗/灯谜 | 综合物理常识 | AI 对话猜谜（语音/文本） |


### 操作指南
#### 基础操作
+ **移动**：WASD 或方向键
+ **交互**：鼠标左键点击 / E 键
+ **语音输入**：长按语音按钮说话
+ **文本输入**：在输入框输入后按 Enter

#### 猜诗探锦关卡详细说明
1. **开始游戏**：点击"开始游戏"按钮，AI 将生成一道融合物理与诗句的谜题
2. **查看线索**：阅读"物理线索"和"诗句线索"提示
3. **提问互动**（可选）：
    - 最多可提问 3 次
    - 只能问封闭式问题（AI 回答"是"或"不是"）
    - 支持语音和文本两种输入方式
4. **选择答案**：点击 A/B/C/D 选项按钮
5. **查看解析**：答对后可查看详细解析，包含诗句出处和物理原理

---

## 📁 项目结构
```plain
dywl/
├── Assets/
│   ├── Script/                    # 核心脚本
│   │   ├── AInpc/                # AI 对话系统
│   │   │   ├── BaiduSpeechTranscribe.cs
│   │   │   ├── BaiduTranslate.cs
│   │   │   └── poetryTalk.cs
│   │   ├── MiGong/               # 迷宫系统
│   │   ├── Cook/                 # 烹饪系统
│   │   ├── touhu/                # 射箭系统
│   │   └── Player/               # 玩家控制
│   ├── Resources/
│   │   └── Prompt.txt            # AI Prompt 模板
│   ├── Scenes/                   # 游戏场景
│   └── [其他资源文件夹]
├── ProjectSettings/              # Unity 项目设置
├── Packages/                     # 包依赖
└── README.md                     # 本文档
```

---

## 🖼️ 游戏截图
1. **主场景概览**

<!-- 这是一张图片，ocr 内容为： -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768578468595-952c78bf-9935-45bd-b81e-91ef73b2bd9d.png)

<!-- 这是一张图片，ocr 内容为：E 这叉鱼的窍门与光有关,光线 入水便发生偏折,若径直刺 之,只能及鱼之虚影.鱼处 -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768578493941-c1f63bb4-c881-404e-a329-e0cad37a5341.png)

2. **“捕鱼备鲜”关卡界面**

<!-- 这是一张图片，ocr 内容为：请从背包中取出鱼叉! 50 -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768578360084-89068d1a-f73a-461e-838b-d6bd8ecd708e.png)

3. **“稻田巧径”关卡界面**

<!-- 这是一张图片，ocr 内容为：收集进度:2/7 稻田巧径 集齐迷宫中的水稻,通过三道鲁班锁机关 -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768578416687-72735147-3961-4821-a714-fb8bc5cc0eb4.png)

<!-- 这是一张图片，ocr 内容为：结构力学 桦卯结构原理,力学平衡稳定 粥 -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768578455178-8ea56cf4-9c5b-4435-95f1-1c4419cce453.png)

4. **“鱼米炊烟”关卡界面**

<!-- 这是一张图片，ocr 内容为：品 正在生火做饭中. 把握好火候(六成到八成刚刚好) 热力学 通过热传递的方式,让食材变熟 长按空格键生火做饭 -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768578318219-46183997-123e-4a0b-9775-8f866dc31e5c.png)

5. **“妙射五毒”关卡界面**

<!-- 这是一张图片，ocr 内容为：当前分数:0 M 剩余箭数:9 风速:26.12 方向:向右 妙射五毒 在60S内按空格射出10支箭,达到60分 38 -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768578376304-203b2672-d5bc-48d5-afc0-a241e7292833.png)

6. **“猜诗探锦”关卡界面**

<!-- 这是一张图片，ocr 内容为：物理线索:月影浮水 面,随波而摇曳. 诗句线索:静水映明 月,随波千万里. 剩余提问:2 浮光跃金静影沉壁 月落马暗霜满天 湖光秋月两相和 明月松间照 老板:你可以向我提三个关于此诗句的 问题.我将回答你是与不是. 小端:是否与动物有关 老板:不是 912 猜诗探锦 请输入文字 向AI老板提问,在诗句迷题中学习物理知识 -->
![](https://cdn.nlark.com/yuque/0/2026/png/51585215/1768578388280-168fe31c-db2c-460d-8950-c5415a585b10.png)

---

## 🔧 开发说明
### 本地开发
1. **克隆仓库**

```bash
git clone [repository-url]
cd dywl
```

2. **打开项目**
    - 使用 Unity Hub 打开项目
    - 确保 Unity 版本为 2022.3.17f1c1
3. **配置 API 密钥**
    - 按照 [百度 API 配置](#-百度-api-配置) 章节修改相关脚本
4. **运行场景**
    - 打开 `Assets/Scenes/` 中的主场景
    - 点击 Play 按钮开始测试

### 构建发布
1. **File → Build Settings**
2. 选择目标平台（PC / Android / iOS）
3. 配置构建设置
4. 点击 "Build" 生成可执行文件

---

## 📚 参考资料
+ [Unity 官方文档](https://docs.unity3d.com/)
+ [百度智能云 API 文档](https://cloud.baidu.com/doc/)
+ [文心大模型 API 文档](https://cloud.baidu.com/doc/WENXINWORKSHOP/s/S8lqb3nrr)
+ [百度语音识别文档](https://cloud.baidu.com/doc/SPEECH/s/8k38lxjfn)

---

## 📄 许可证
本项目为教育用途开发，具体许可证信息待定。

---

## 👥 贡献者
+ 项目开发团队：Erd、Evan Lu、Ming

---

## 📮 联系方式
如有问题或建议，请通过以下方式联系：

+ 项目 Issues：[[https://github.com/Erd-omg/DuanYangWuLi/issues](https://github.com/Erd-omg/DuanYangWuLi/issues)]

---

**端阳悟理** - 让物理知识在传统文化中焕发新光 Made with ❤️ using Unity & Baidu AI 

