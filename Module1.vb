
Option Compare Text
Module Module1


    Sub Main()
        Dim Vcc, VD1, Rgen, R17, R18, R19, R20, R21, R22, VR18, VR19, VR20, VR21, VR22, VR24, IR18, IR19, IR20, IR21, IR22, IB1, MathTest, VBE As Decimal
        Dim BetaQ4, BetaQ5, BetaQ6, BetaDarlington, IBQ4, ICQ4, IEQ4, IBQ5, ICQ5, IEQ5, IBQ6, ICQ6, IEQ6 As Decimal
        Dim rPrimeEQ4, rPrimeEQ5, rPrimeEQ6, VoutQ4, VinQ4, AVQ4, VinDarlington, VoutDarlington, AVDarlington, OverallAV As Decimal
        Dim Userinput As String

        VBE = 0.7

        Format("Scientific")

        Console.WriteLine("Manual mode? Y/N")
        Userinput = Console.ReadLine()
        If Userinput = "y" Then

            Console.WriteLine("V++ =")
            Vcc = Console.ReadLine()

            Console.WriteLine("VD1 =")
            VD1 = Console.ReadLine()

            Console.WriteLine("RGen =")
            Rgen = Console.ReadLine()

            Console.WriteLine("R17 =")
            R17 = Console.ReadLine()

            Console.WriteLine("R18 =")
            R18 = Console.ReadLine()

            Console.WriteLine("R19 =")
            R19 = Console.ReadLine()

            Console.WriteLine("R20 =")
            R20 = Console.ReadLine()

            Console.WriteLine("R21 =")
            R21 = Console.ReadLine()

            Console.WriteLine("R22 =")
            R22 = Console.ReadLine()

        Else
            Vcc = 25
            VD1 = 0.7
            Rgen = 50
            R17 = 100
            R18 = 2.2 * 10 ^ 3
            R19 = 9.97 * 10 ^ 3
            R20 = 9.97 * 10 ^ 3
            R21 = 100.3 * 10 ^ 3
            R22 = 219
        End If

        IB1 = (Vcc - VD1) / (R19 + R20)
        Console.WriteLine($"IB1 = {IB1}")

        IR19 = IB1
        IR20 = IB1

        'IB1 is the DC current sent through the branch through R19 and R20

        VR19 = IB1 * R19
        Console.WriteLine($"VR19 = { VR19}")

        VR20 = IB1 * R19
        Console.WriteLine($"VR20 = { VR20}")

        VR21 = VBE + VBE
        Console.WriteLine($"VR24 = { VR21}")

        IR21 = VR21 / R21
        Console.WriteLine($"IR24 = { IR21}")

        VR22 = (Vcc - VBE - VR20 - 0.0463477)
        'the 0.0463477 is from the current backwards calculated 
        Console.WriteLine($"VR22 = { VR22}")

        IR22 = VR22 / R22
        Console.WriteLine($"IR22 = { IR22}")
        Console.WriteLine("IR22 = IC of Darlington Pair")

        Console.WriteLine("Manual mode for Transistor Beta Entry? Y/N")
        Userinput = Console.ReadLine()
        If Userinput = "y" Then
            Console.WriteLine($"Beta Q4?")
            BetaQ4 = Console.ReadLine

            Console.WriteLine($"Beta Q5?")
            BetaQ5 = Console.ReadLine

            Console.WriteLine($"Beta Q6?")
            BetaQ6 = Console.ReadLine
        Else
            BetaQ4 = 250
            BetaQ5 = 250
            BetaQ6 = 30
        End If

        BetaDarlington = BetaQ5 * BetaQ6

        'IBQ5 = IR22 / BetaDarlington
        IBQ5 = IR21 / 10
        Console.WriteLine($"IBQ5 = { IBQ5}")

        ICQ5 = IBQ5 * BetaQ5
        Console.WriteLine($"ICQ5 = { ICQ5}")

        IEQ5 = IBQ5 * (BetaQ5 + 1)
        Console.WriteLine($"IEQ5 = { IEQ5}")

        IBQ6 = IEQ5
        Console.WriteLine($"IBQ6= { IBQ6}")

        ICQ6 = IBQ6 * BetaQ6
        Console.WriteLine($"ICQ6 = { ICQ6}")

        IEQ6 = IBQ6 * (BetaQ6 + 1)
        Console.WriteLine($"IEQ6 = { IEQ6}")

        ICQ4 = IR21 + IBQ5
        Console.WriteLine($"ICQ4 = { ICQ4}")

        IBQ4 = ICQ4 / BetaQ4
        Console.WriteLine($"IBQ4 = { IBQ4}")

        IEQ4 = IBQ4 * (BetaQ4 + 1)
        Console.WriteLine($"IEQ4 = { IEQ4}")

        VR18 = IEQ4 * R18
        Console.WriteLine($"VR18 = { VR18}")

        rPrimeEQ4 = 0.026 / IEQ4
        Console.WriteLine($"r'eQ4 = { rPrimeEQ4}")

        rPrimeEQ5 = 0.026 / IEQ5
        Console.WriteLine($"r'eQ5 = { rPrimeEQ5}")

        rPrimeEQ6 = 0.026 / IEQ6
        Console.WriteLine($"r'eQ6 = { rPrimeEQ6}")

        Console.WriteLine("This Part can change based on sourcecode")
        Console.ReadLine()

        'These formulas are based of what Lane and Rob did Together.


        VoutQ4 = (R21 ^ -1 + (BetaQ5 * (rPrimeEQ5 + (BetaQ6 * rPrimeEQ6))) ^ -1) ^ -1
        Console.WriteLine($"VoutQ4 (In Terms of Resistance) = { VoutQ4} ohms")
        'R21//(BetaQ5*(r'eQ5 + (BetaQ6)*(r'eQ6)))

        VinQ4 = ((R18 + R22) ^ -1 + (rPrimeEQ4) ^ -1) ^ -1
        Console.WriteLine($"VinQ4 (In Terms of Resistance) = { VinQ4} ohms")
        '((R18+R22)//(r'eQ4))

        AVQ4 = VoutQ4 / VinQ4
        Console.WriteLine($"AVQ4 = { AVQ4}")


        VoutDarlington = ((R22 ^ -1) + (R18 + (rPrimeEQ4 ^ -1 + (R17 + Rgen) ^ -1) ^ -1) ^ -1) ^ -1
        Console.WriteLine($"VoutDarlington (in terms of Resistance) = { VoutDarlington} Ohms")
        'R22//(R18+(R17+R18)//r'eQ4))

        VinDarlington = rPrimeEQ5 + ((BetaQ6) * (rPrimeEQ6))
        Console.WriteLine($"VinDarlington (in terms of Resistance) = { VinDarlington} Ohms")
        'r'eQ5 +(BetaQ6)(r'eQ6)

        AVDarlington = VoutDarlington / VinDarlington
        Console.WriteLine($"AVDarlington = { AVDarlington}")

        OverallAV = AVDarlington * AVQ4
        Console.WriteLine($"Overall Gain = { OverallAV}")



        Console.ReadLine()






    End Sub

End Module
