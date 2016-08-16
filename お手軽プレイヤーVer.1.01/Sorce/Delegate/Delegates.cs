using System;

namespace LightPlayer
{
    /// <summary>
    /// 別スレッドからフォーム上のコントールを操作する際に呼び出す
    /// Form#Invoke()メソッドを受け取るためのデリゲード
    /// </summary>
    public delegate object InvolkeWorker( Action action );
}