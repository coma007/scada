﻿using scada_back.Infrastructure.Feature.Tag.Model.Abstraction;
using scada_back.Infrastructure.Feature.TagHistory;

namespace scada_back.Api.WebSocket;

public interface IWebSocketServer
{
    void NotifyClientAboutNewRecord(TagHistoryRecord record);
    void NotifyProcessingAppAboutNewTag(Tag tag);
}