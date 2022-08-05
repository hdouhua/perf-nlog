# NLog performance testing

the project is inspired by [this project](https://github.com/imanushin/nlog-log4net-performance-comparison).

recommended setting for project

```xml
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      autoReload="true" throwExceptions="false" throwConfigExceptions="true">

  <!--<variable name="basedir" value="../logs/"/>-->
  <variable name="folder" value="${gdc:item=basedir}${shortdate}" />

  <extensions>
    <add assembly="NLog.Web.AspNetCore"/>
  </extensions>

  <targets>
    <default-wrapper xsi:type="AsyncWrapper"
      overflowAction="Block"
      queueLimit="5000"
      batchSize="200"
      timeToSleepBetweenBatches="1"
    />
    <default-target-parameters xsi:type="File"
      encoding="utf-8"
      archiveNumbering="Sequence"
      archiveAboveSize="10240000"
      maxArchiveFiles="10000"
      keepFileOpen="true"
      concurrentWrites="true"
      layout="${longdate}|${threadid}|${level}|${logger}|${aspnet-TraceIdentifier:ignoreActivityId=true}|${message} ${exception:format=tostring}"
    />

    <target xsi:type="File" name="errorFile"
      fileName="${folder}/error.log"
      archiveFileName="${folder}/error.{#}.log" />

    <target xsi:type="File" name="appFile"
      fileName="${folder}/app.log"
      archiveFileName="${folder}/app.{#}.log" />

  </targets>

  <!-- rules to map from logger name to target -->
  <rules>
    <logger name="Microsoft.*" maxlevel="Info" final="true" />
    <logger name="*" minlevel="Error" writeTo="errorFile" final="true" />
    <logger name="*" minlevel="Trace" writeTo="appFile">
      <filters>
        <when condition="length('${message}') > 1024" action="Ignore" />
      </filters>
    </logger>
  </rules>

</nlog>
```

## reference

- [Configure from code](https://github.com/NLog/NLog/wiki/Configure-from-code)
- [File target](https://github.com/NLog/NLog/wiki/File-target)
- [AsyncWrapper target](https://github.com/NLog/NLog/wiki/AsyncWrapper-target)
- [Performance](https://github.com/NLog/NLog/wiki/Performance)
- [Nlog best practices](hhttps://github.com/NLog/NLog/wiki/Tutorial#best-practices-for-using-nlog)
