using System;
using System.Collections.Generic;
using System.Text;

namespace w9wen.Lamp.BE.API
{
    public class ResponseEntity<T>
    {
        public string Message { get; set; }

        public string DetailMessage { get; set; }

        public T Result { get; set; }

        public ResponseStatusEnum Status { get; set; }
    }
}