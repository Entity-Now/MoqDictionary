# MoqDictionary

一款基于c#的字典库，内含340万+离线单词，并且支持调用线上平台进行翻译。

## 使用方法

### 离线单词

> 单个单词

```cs
QueryDir.getDir("Hello");
```

> 常用与分割编程中的变量、函数、类名等

```cs
var words = ParseString.getWordArray("hello world");
QueryDir.getDir(words);
```

> 可用于分割字符串中的单词

```cs
var words = ParseString.getWordAtText("hello world");
QueryDir.getDir(words);
```

## 支持的在线平台

由于都是免费版本，且用且珍惜。

| 平台 |
| --- |
| Google |
| Bind |
| Deep |
| Yandex |
