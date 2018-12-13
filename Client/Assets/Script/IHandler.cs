using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//注： 每个模块类都会继承该接口，每个模块都会封装他的 MessageReceive 函数

    public interface IHandler          
    {
        void MessageReceive(CSMsg model);
    }

