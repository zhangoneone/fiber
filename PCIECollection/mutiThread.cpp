#include "StdAfx.h"
#include <iostream>
#include <windows.h>
using namespace std;
/*
HANDLE hMutex = NULL;//互斥量
//线程函数
DWORD WINAPI Fun(LPVOID lpParamter)
{
    for (int i = 0; i < 10; i++)
    {
        //请求一个互斥量锁
        WaitForSingleObject(hMutex, INFINITE);
        cout << "A Thread Fun Display!" << endl;
        Sleep(100);
        //释放互斥量锁
        ReleaseMutex(hMutex);
    }
    return 0L;//表示返回的是long型的0

}

int main()
{
    //创建一个子线程
    HANDLE hThread = CreateThread(NULL, 0, Fun, NULL, 0, NULL);
    hMutex = CreateMutex(NULL, FALSE,"screen");
    //关闭线程
    CloseHandle(hThread);
    //主线程的执行路径
    for (int i = 0; i < 10; i++)
    {
        //请求获得一个互斥量锁
        WaitForSingleObject(hMutex,INFINITE);
        cout << "Main Thread Display!" << endl;
        Sleep(100);
        //释放互斥量锁
        ReleaseMutex(hMutex);
    }
    return 0;
}
*/