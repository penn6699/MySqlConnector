using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// 
/// </summary>
public class JsonHelper
{
    /// <summary>
    /// 将对象序列化为JSON格式
    /// </summary>
    /// <param name="obj">对象</param>
    /// <returns>json字符串</returns>
    public static string Serialize(object obj)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
        return JsonConvert.SerializeObject(obj, settings);
    }

    /// <summary>
    /// 将对象序列化为JSON格式
    /// </summary>
    /// <param name="obj">对象</param>
    /// <returns>json字符串</returns>
    public static string Serialize(object obj, JsonSerializerSettings settings)
    {
        return JsonConvert.SerializeObject(obj, settings);
    }

    /// <summary>
    /// 解析JSON字符串生成对象实体
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="json">json字符串。例子：[{"ID":"1","Name":"123"}]</param>
    /// <returns>对象实体</returns>
    public static T Deserialize<T>(string json) where T : class
    {
        JsonSerializer serializer = new JsonSerializer();
        StringReader sr = new StringReader(json);
        return serializer.Deserialize(new JsonTextReader(sr), typeof(T)) as T;
    }

    /// <summary>
    /// 解析JSON数组字符串生成对象实体集合
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="json">json数组字符串。例子：[{"ID":"1","Name":"123"}]</param>
    /// <returns>对象实体集合</returns>
    public static List<T> DeserializeToList<T>(string json)
    {
        JsonSerializer serializer = new JsonSerializer();
        StringReader sr = new StringReader(json);
        List<T> list = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>)) as List<T>;
        return list;
    }

    /// <summary>
    /// 反序列化JSON字符串给定的匿名对象。使用例子：JsonHelper.DeserializeAnonymousType(resultJsonString, new { success = false,msg="" });
    /// </summary>
    /// <typeparam name="T">匿名对象类型</typeparam>
    /// <param name="json">json字符串</param>
    /// <param name="anonymousTypeObject">匿名对象</param>
    /// <returns>匿名对象</returns>
    public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
    {
        T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
        return t;
    }

}

