using System.Runtime.Serialization;

namespace SearchCs {
    [DataContract]
    class Request {
        [DataMember]
        public string Pattern { set; get; }

        [DataMember]
        public string Path { set; get; }
    }
}
