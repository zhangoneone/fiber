#include "StdAfx.h"
#include <iostream>
#include <windows.h>
using namespace std;
/*
HANDLE hMutex = NULL;//������
//�̺߳���
DWORD WINAPI Fun(LPVOID lpParamter)
{
    for (int i = 0; i < 10; i++)
    {
        //����һ����������
        WaitForSingleObject(hMutex, INFINITE);
        cout << "A Thread Fun Display!" << endl;
        Sleep(100);
        //�ͷŻ�������
        ReleaseMutex(hMutex);
    }
    return 0L;//��ʾ���ص���long�͵�0

}

int main()
{
    //����һ�����߳�
    HANDLE hThread = CreateThread(NULL, 0, Fun, NULL, 0, NULL);
    hMutex = CreateMutex(NULL, FALSE,"screen");
    //�ر��߳�
    CloseHandle(hThread);
    //���̵߳�ִ��·��
    for (int i = 0; i < 10; i++)
    {
        //������һ����������
        WaitForSingleObject(hMutex,INFINITE);
        cout << "Main Thread Display!" << endl;
        Sleep(100);
        //�ͷŻ�������
        ReleaseMutex(hMutex);
    }
    return 0;
}
*/