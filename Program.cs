using System;
using System.Text;

namespace GameBuilderPattern
{
    // -----------------------------------------------------------
    // PRODUCT: Клас, що представляє складний об'єкт "Комп'ютерна Гра"
    // -----------------------------------------------------------
    public class ComputerGame
    {
        // Приватні поля (Інкапсуляція)
        private string _graphics;
        private string _sound;
        private string _storyLine;

        // Публічні методи для встановлення значень (використовуються Будівельником)
        public void SetGraphics(string graphics)
        {
            _graphics = graphics;
        }

        public void SetSound(string sound)
        {
            _sound = sound;
        }

        public void SetStoryLine(string storyLine)
        {
            _storyLine = storyLine;
        }

        // Метод для виведення інформації про гру
        public override string ToString()
        {
            return $"--- Конфігурація Гри ---\n" +
                   $"Графіка: {_graphics}\n" +
                   $"Звук:    {_sound}\n" +
                   $"Сюжет:   {_storyLine}\n";
        }
    }

    // -----------------------------------------------------------
    // ABSTRACT BUILDER: Визначає інтерфейс для створення частин гри
    // -----------------------------------------------------------
    public abstract class GameBuilder
    {
        protected ComputerGame _computerGame;

        public void CreateNewGame()
        {
            _computerGame = new ComputerGame();
        }

        public ComputerGame GetGame()
        {
            return _computerGame;
        }

        // Абстрактні методи, які повинні реалізувати конкретні будівельники
        public abstract void BuildGraphics();
        public abstract void BuildSound();
        public abstract void BuildStoryLine();
    }

    // -----------------------------------------------------------
    // CONCRETE BUILDER 1: Створює гру класу AAA (блокбастер)
    // -----------------------------------------------------------
    public class TripleAGameBuilder : GameBuilder
    {
        public override void BuildGraphics()
        {
            _computerGame.SetGraphics("Ultra 4K, Ray Tracing");
        }

        public override void BuildSound()
        {
            _computerGame.SetSound("Dolby Atmos 7.1 Surround");
        }

        public override void BuildStoryLine()
        {
            _computerGame.SetStoryLine("Епічна сага з нелінійним сюжетом");
        }
    }

    // -----------------------------------------------------------
    // CONCRETE BUILDER 2: Створює просту інді-гру
    // -----------------------------------------------------------
    public class IndieGameBuilder : GameBuilder
    {
        public override void BuildGraphics()
        {
            _computerGame.SetGraphics("Піксель-арт (Retro Style)");
        }

        public override void BuildSound()
        {
            _computerGame.SetSound("8-bit Chiptune Stereo");
        }

        public override void BuildStoryLine()
        {
            _computerGame.SetStoryLine("Коротка філософська історія");
        }
    }

    // -----------------------------------------------------------
    // DIRECTOR: Керує процесом будівництва (Code Convention & Encapsulation)
    // -----------------------------------------------------------
    public class GameDirector
    {
        private GameBuilder _gameBuilder;

        // Встановлення будівельника через метод (або конструктор)
        public void SetBuilder(GameBuilder builder)
        {
            _gameBuilder = builder;
        }

        // Алгоритм створення гри завжди однаковий, але деталі різні
        public ComputerGame ConstructGame()
        {
            if (_gameBuilder == null)
            {
                throw new InvalidOperationException("Будівельник не встановлено!");
            }

            _gameBuilder.CreateNewGame();
            _gameBuilder.BuildGraphics();
            _gameBuilder.BuildSound();
            _gameBuilder.BuildStoryLine();

            return _gameBuilder.GetGame();
        }
    }

    // -----------------------------------------------------------
    // CLIENT: Точка входу
    // -----------------------------------------------------------
    class Program
    {
        static void Main(string[] args)
        {
            // Налаштування кодування для коректного відображення кирилиці
            Console.OutputEncoding = Encoding.UTF8;

            // 1. Створюємо Директора
            GameDirector director = new GameDirector();

            // 2. Створюємо першого будівельника (AAA гра)
            GameBuilder tripleABuilder = new TripleAGameBuilder();

            // 3. Директор будує гру, використовуючи цей будівельник
            director.SetBuilder(tripleABuilder);
            ComputerGame myExpensiveGame = director.ConstructGame();

            Console.WriteLine("Створено гру #1 (AAA Project):");
            Console.WriteLine(myExpensiveGame.ToString());

            // -----------------------------------------------

            // 4. Створюємо другого будівельника (Інді гра)
            GameBuilder indieBuilder = new IndieGameBuilder();

            // 5. Змінюємо будівельника у директора
            director.SetBuilder(indieBuilder);
            ComputerGame myIndieGame = director.ConstructGame();

            Console.WriteLine("Створено гру #2 (Indie Project):");
            Console.WriteLine(myIndieGame.ToString());

            Console.ReadKey();
        }
    }
}