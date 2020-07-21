using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using static System.Console;

namespace yield_return
{
    class Program
    {
        public static void Main()
        {
            WriteLine("Antes de chamar 'Foo'....");
            var foo = Foo();
            WriteLine("Depois de chamar 'Foo'....");

            foreach (var elem in foo)
            {
                WriteLine($"Antes de imprimir 'elem' {elem}...");
                WriteLine(elem);
                WriteLine($"Depois de imprimir 'elem'{elem}...");
            }
        }

        public static IEnumerable<int> Foo() => new MyEnumerable();

        public class MyEnumerable : IEnumerable<int>, IDisposable
        {
            public void Dispose() { }

            //Para implementação devemos retornar a instância do MyEnumerator
            public IEnumerator<int> GetEnumerator() => new MyEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => new MyEnumerator();
        }

        public class MyEnumerator : IEnumerator<int>
        {
            //Implementação com Generics 
            //Manter um backfield
            public MyEnumerator()
            {
                WriteLine("Antes de iniciar o 'loop for'... ");
            }
            int _current = -1;
            //Não podemos implementar os métodos {get; set;} para não quebrar a interface
            //propriedade e objeto retornam _current ao invés de "throw new NotImplementedException();"
            public int Current => _current;

            object IEnumerator.Current => _current;
            //Por questões de compatibilidade retroativa a Microsoft mantém uma interface com generics e outra sem generics 
            //Abaixo os códigos para a implementação da interface Enumerator(interface antiga) 
            //IEnumerator já possui implementação do Dispose, portanto, não será utilizado.
            public void Dispose() 
            { 
                WriteLine("Depois de encerrar o 'loop for'..."); 
            }

            //Implementação sem Generics
            public bool MoveNext() 
            {
                if(_current >= 0) 
                {
                    WriteLine($"Depois do 'yield return {_current}'...");
                }

                if (_current >= 4) return false;
                WriteLine($"Antes do 'yield return {_current}'...");
                _current++;
                return true;
            }

            public void Reset()
            {
                _current = -1;
            }
        }
    }
}
