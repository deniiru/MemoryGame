using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MemoryGame2.Commands
{
            class RelayCommand : ICommand
            {
                /*commandTask este comanda care se executa - in cazul nostru Add, Substract, Multiply sau Divide
                Action este un delegat implicit definit in .NET Framework
                poate fi parametrizat (cu object ca in cazul de fata) sau neparametrizat


                Action este reprezentat printr-o metoda care returneaza void
                daca e neparametrizat, atunci metoda care il inlocuieste nu trebuie sa aiba parametru
                daca e parametrizat(cu tipul object ca in cazul de fata), atunci metoda care il inlocuieste
                trebuie sa aiba un parametru de tipul object (vezi metodele Add, Substract, Multiply si Divide
                care au cate un parametru de tip object si returneaza void)

                In aceasta aplicatie nu aveam nevoie de parametru la metode si puteam foarte bine sa folosim
                delegatul Action fara parametrizare*/

                private Action<object> commandTask;

                /*canExecuteTask este valoarea te tip boolean care imi spune daca pot sau nu sa execut comanda
                 * Predicate este tot un delegat implicit definit de .NET Framework
                 * acesta este obligatoriu parametrizat si este reprezentat printr-o metoda care returneaza bool
                 * metoda care il inlocuieste (aici metoda CanExecute) trebuie sa aiba un parametru de tipul
                 * parametrului delegatului(object in cazul nostru)*/

                private Predicate<object> canExecuteTask;

                public RelayCommand(Action<object> workToDo, Predicate<object> canExecute)
                {
                    commandTask = workToDo;
                    canExecuteTask = canExecute;
                }

                /*Constructorul cu un singur parametru, in locul parametrului Predicate apeleaza o metoda care
                returneaza "true" - si anume metoda DefaultCanExecute*/

                public RelayCommand(Action<object> workToDo)
                    : this(workToDo, DefaultCanExecute)
                {
                    commandTask = workToDo;
                }

                /*Metoda apelata pe post de Predicate pt constructorul cu un singur parametru
                aceasta returneaza valoarea true hardcodat*/

                private static bool DefaultCanExecute(object parameter)
                {
                    return true;
                }

                /*Metoda care se apeleaza in cazul in care se executa metoda Execute si stabileste in functie de 
                valoarea atributului de tip Predicate<object>, canExecuteTask, daca se poate executa comanda*/

                public bool CanExecute(object parameter)
                {
                    return canExecuteTask != null && canExecuteTask(parameter);
                }

                /*CanExecuteChanged este un eveniment care schimba starea componentei grafice care doreste 
                 * executia comenzii in cazul in care CanExecute returneaza false, acest eveniment dezactiveaza 
                 * componenta grafica - vezi valorile pe cere le ia atributul Enabled al butoanelor
                 * 
                 * RequerySuggested - este un eveniment static din clasa CommandManager care detecteaza
                 * situatiile care pot schimba abilitatea de a se executa a unei comenzi*/

                public event EventHandler CanExecuteChanged
                {
                    add
                    {
                        //+=asociaza un handler la un eveniment
                        CommandManager.RequerySuggested += value;
                    }
                    remove
                    {
                        //-=sterge un handler de la un eveniment
                        CommandManager.RequerySuggested -= value;
                    }
                }

                /*Este responsabil de executia metodei reprezentate prin Action;
                daca Action nu era parametrizat, atunci apelul ar fi fost commandTask();
                inainte de apelul lui Execute se apeleaza automat CanExecute pentru a vedea daca metoda se
                poate executa*/

                public void Execute(object parameter)
                {
                    commandTask(parameter);
                }
            }

}
