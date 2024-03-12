using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaChat.Models
{
    public class ParametersModel
    {
        public MessageQuery[] Messages { get; set; } = Array.Empty<MessageQuery>();
        /// <summary>
        /// Возможные значения: [GigaChat, GigaChat:latest, GigaChat-Plus, GigaChat-Pro]
        /// </summary>
        public string Model { get; set; } = "GigaChat:latest";
        /// <summary>
        /// Чем выше значение, тем более случайным будет ответ модели. 
        /// Значение > 2 ответ может отличаться избыточной случайностью
        /// </summary>
        public float Temperature { get; set; } = 0.1f;
        /// <summary>
        /// Задает вероятностную массу токенов, которые должна учитывать модель. 
        /// Так, если передать значение 0.1, модель будет учитывать только токены, чья вероятностная масса входит в верхние 10%.
        /// </summary>
        public float Top_p { get; set; } = 0.4f;
        /// <summary>
        /// >=1 и <=4
        /// Количество вариантов ответов, которые нужно сгенерировать для каждого входного сообщения.
        /// </summary>
        public int n { get; set; } = 1;
        /// <summary>
        /// По умолчанию: 1024
        /// Максимальное количество токенов, которые будут использованы для создания ответов
        /// </summary>
        public int MaxTokens { get; set; } = 1024;
        /// <summary>
        /// Кол-во повторений слов
        /// </summary>
        public float RapetitionPenalty { get; set; } = 2f;
        /// <summary>
        /// Передача сообщения по частям в потоке. Если нужно
        /// </summary>
        /*public bool Stream {  get; set; }*/
        /// <summary>
        /// Интервал в секундах, который проходит между отправкой токенов
        /// </summary>
        /*public int update_interval { get; set; }*/

    }
}
