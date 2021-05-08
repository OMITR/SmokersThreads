using System;
using System.Threading;

namespace CigaretteSmokers
{
    class CigaretteSmokers
    {
        private static Mutex mutex = new Mutex(false);
        private static AutoResetEvent signalFromServant1 = new AutoResetEvent(false);
        private static AutoResetEvent signalFromServant2 = new AutoResetEvent(false);
        private static AutoResetEvent signalFromServant3 = new AutoResetEvent(false);

        private static void Main(string[] args)
        {
            Thread StartServant = new Thread(startServant);
            Thread SmokerMatch = new Thread(smokerMatch);
            Thread SmokerTobacco = new Thread(smokerTobacco);
            Thread SmokerPaper = new Thread(smokerPaper);

            SmokerMatch.Start();
            SmokerTobacco.Start();
            SmokerPaper.Start();
            StartServant.Start();
        }

        static void startServant()
        {
            while (true)
            {
                mutex.WaitOne();
                int random = new Random().Next(1, 4);

                if (random == 1)
                {
                    Console.WriteLine("Слуга взял бумагу и табак и положил их на стол.\n");
                    Thread.Sleep(new Random().Next(100, 2000));

                    signalFromServant1.Set();

                }
                else if (random == 2)
                {
                    Console.WriteLine("Слуга взял бумагу и спички и положил их на стол.\n");
                    Thread.Sleep(new Random().Next(100, 2000));

                    signalFromServant2.Set();
                }
                else
                {
                    Console.WriteLine("Слуга взял спички и табак и положил их на стол.\n");
                    Thread.Sleep(new Random().Next(100, 2000));

                    signalFromServant3.Set();
                }

                mutex.ReleaseMutex();
            }
        }

        static void smokerMatch()
        {
            while (true)
            {
                signalFromServant1.WaitOne();
                mutex.WaitOne();

                Console.WriteLine("Курильщик со спичками взял табак и бумагу со стола и начал скручивать сигарету.\n");

                mutex.ReleaseMutex();
                signalFromServant1.Reset();

                Thread.Sleep(new Random().Next(100, 2000));
                Console.WriteLine("Курильщик со спичками закурил.\n");
                Thread.Sleep(new Random().Next(100, 2000));
                Console.WriteLine("Курильщик со спичками выкурил сигарету.\n");
            }
        }

        static void smokerTobacco()
        {
            while (true)
            {
                signalFromServant2.WaitOne();
                mutex.WaitOne();

                Console.WriteLine("Курильщик с табаком взял бумагу и спички со стола и начал скручивать сигарету.\n");

                mutex.ReleaseMutex();
                signalFromServant2.Reset();

                Thread.Sleep(new Random().Next(100, 2000));
                Console.WriteLine("Курильщик с табаком закурил.\n");
                Thread.Sleep(new Random().Next(100, 2000));
                Console.WriteLine("Курильщик с табаком выкурил сигарету.\n");
            }
        }

        static void smokerPaper()
        {
            while (true)
            {
                signalFromServant3.WaitOne();
                mutex.WaitOne();

                Console.WriteLine("Курильщик с бумагой взял табак и спички со стола и начал скручивать сигарету.\n");

                mutex.ReleaseMutex();
                signalFromServant3.Reset();

                Thread.Sleep(new Random().Next(100, 2000));
                Console.WriteLine("Курильщик с бумагой закурил.\n");
                Thread.Sleep(new Random().Next(100, 2000));
                Console.WriteLine("Курильщик с бумагой выкурил сигарету.\n");
            }
        }
    }
}