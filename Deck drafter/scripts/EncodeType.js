function encode(str) {
    var output = '';
    for (var i = 0; i < str.length; i += 8) {
        var segment = str.substring(i, i + 8);
        if (segment.length < 8) {
            segment = segment.padEnd(8, '0');
        }
        output += String.fromCharCode(parseInt(segment, 2));
    }
    return (btoa(output));
}
//void accessJava()
//{
//    ScriptControl js = new ScriptControl();
// js.AllowUI = false;
// js.Language = "JScript";
// js.Reset();
// js.AddCode(@"
//      function test(x) {
//return x + 42;
//  }
//  ");
// object[] parms = new object[] { 11 };
//      int result = (int)js.Run("test", ref parms);
//  Console.WriteLine(result);
//}
//void accessType(string i)
//{
//   EncodeType.encode(i);
//}
//# sourceMappingURL=EncodeType.js.map