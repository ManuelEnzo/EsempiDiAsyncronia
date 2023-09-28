Imports System

Module Program
    Sub Main(args As String())
        '-------------- L'struzione dopo il task viene eseguita prima
        'Task.Run(Sub()
        '             For i = 1 To 10
        '                 Console.WriteLine(i)
        '             Next
        '         End Sub)
        'Console.WriteLine("Dopo Task")

        '------------- Il secondo Task verrà eseguito solamente al termine del primo
        'Task.Run(Sub()
        '             For i = 1 To 900
        '                 Console.WriteLine(i)
        '             Next
        '         End Sub).ContinueWith(Sub()
        '                                   For i = 901 To 902
        '                                       Console.WriteLine(i)
        '                                   Next
        '                               End Sub)
        '------------ Il primo Task viene eseguito sempre prima del secondo
        '------------ ma la istruzioni successive non attendono il completamento dei Task
        'Task.Run(Sub()
        '             For i = 1 To 2000
        '                 Console.WriteLine(i)
        '             Next
        '         End Sub).ContinueWith(Sub()
        '                                   For i = 2001 To 2020
        '                                       Console.WriteLine(i)
        '                                   Next
        '                               End Sub)

        'Console.WriteLine("Dopo Task")

        '----------- Senza un metodo await viene eseguita l'istruzione successiva
        'Dim t = EseguiOperazioneAsincrona()
        'Console.WriteLine("Dopo Task")

        '----------- In questo modo attendo il completamento del Task sfruttando l'asincronia
        'Dim t = EseguiOperazioneAsincrona()
        't.GetAwaiter().GetResult()
        'Console.WriteLine("Dopo Task")

        '----------- In questo modo attendo che venga terminata l'esecuzione di Test
        'Dim t = EseguiOperazioneAsincrona2Task()
        't.GetAwaiter().GetResult()
        'Console.WriteLine("Dopo Task")

        '----------- In questa maniera viene eseguita prima l'istruzione successiva
        'Dim t = EseguiOperazioneAsincrona2Task()
        't.ContinueWith(Sub()
        '                   Task.Run(Sub()
        '                                Test()
        '                            End Sub)
        '               End Sub)

        'Console.WriteLine("Dopo Task")

        '---------- In questa maniera viene eseguita l'operazione successiva e poi partono i Task
        'Dim t = EseguiOperazioneAsincrona()
        't.ContinueWith(Sub()
        '                   Task.Run(Sub()
        '                                Test()
        '                            End Sub)
        '               End Sub).ConfigureAwait(True)

        'Console.WriteLine("Dopo Task")

        '--------- Le operazioni successive vengono eseguite senza attendere
        'Dim t = EseguiOperazioneAsincrona()
        't.ContinueWith(Sub()
        '                   Task.Run(Sub()
        '                                Test()
        '                            End Sub)
        '               End Sub).ConfigureAwait(False)

        'Console.WriteLine("Dopo Task")

        '-------- In Questa maniera l'istruzione successiva verrà eseguita solo al termine dei Tasks
        Dim t = EseguiOperazioneAsincrona()
        t.ContinueWith(Sub(previousTask)
                           Console.WriteLine("Operazione asincrona completata")

                           Dim result As Boolean = Test()

                       End Sub).Wait()

        Console.WriteLine("Dopo Task")


        Console.Read()
    End Sub

    Async Function EseguiOperazioneAsincrona() As Task
        Await Task.Run(Sub()
                           For i = 1 To 10
                               Console.WriteLine(i)
                               Task.Delay(100)
                           Next
                       End Sub)
    End Function

    Async Function EseguiOperazioneAsincrona2Task() As Task
        Await Task.Run(Sub()
                           Test()
                       End Sub)
    End Function

    Private Function Test() As Boolean
        For i = 11 To 15
            Console.WriteLine(i)
        Next
        Return True
    End Function
End Module
