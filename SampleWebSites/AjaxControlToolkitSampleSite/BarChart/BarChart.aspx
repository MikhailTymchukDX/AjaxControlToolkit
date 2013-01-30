﻿<%@ Page Language="C#" MasterPageFile="~/DefaultMaster.master" AutoEventWireup="true"
    CodeFile="BarChart.aspx.cs" Inherits="BarChart_BarChart" Title="BarChart Sample"
    Culture="auto" UICulture="auto" 
    Theme="SampleSiteTheme" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />    
    <div class="demoarea">
        <div class="demoheading">
            BarChart Demonstration</div>        
        <br />        
        <ajaxToolkit:BarChart ID="Chart1" runat="server" ChartHeight="300" ChartWidth="450" ChartTitle="United States versus European Widget Production" CategoriesAxis="2007,2008,2009,2010,2011,2012"
        ChartType="StackedColumn" TitleColor="#0E426C" CategoryAxisLineColor="#D08AD9" ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB">
        <Series>
            <ajaxToolkit:BarChartSeries Name="United States" BarColor="#6C1E83" Data="110, 189, 255, 95, 107, 140" >
                <%--<DataValues>
                    <ajaxToolkit:DataValue Data="110" />
                    <ajaxToolkit:DataValue Data="189" />
                    <ajaxToolkit:DataValue Data="255" />
                    <ajaxToolkit:DataValue Data="95" />
                    <ajaxToolkit:DataValue Data="107" />
                    <ajaxToolkit:DataValue Data="140" />
                </DataValues>--%>
            </ajaxToolkit:BarChartSeries>
            <ajaxToolkit:BarChartSeries Name="Europe"  BarColor="#D08AD9" Data="49, 77, 95, 68, 70, 79" >
                <%--<DataValues>
                    <ajaxToolkit:DataValue Data="49" />
                    <ajaxToolkit:DataValue Data="77" />
                    <ajaxToolkit:DataValue Data="95" />
                    <ajaxToolkit:DataValue Data="68" />
                    <ajaxToolkit:DataValue Data="70" />
                    <ajaxToolkit:DataValue Data="79" />
                </DataValues>--%>
            </ajaxToolkit:BarChartSeries>
        </Series>
        </ajaxToolkit:BarChart>
        <br />        
    </div>
    <div class="demobottom">
    </div>
    <asp:Panel ID="Description_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Description_ToggleImage" runat="server" ImageUrl="~/images/collapse.jpg"
                AlternateText="collapse" />
            BarChart Description
        </div>
    </asp:Panel>
    <asp:Panel ID="Description_ContentPanel" runat="server" Style="overflow: hidden;">
        <p>
            The BarChart control enables you to render a bar chart from one or more series of values. 
            This control is compatible with any browser which supports SVG including Internet Explorer 9 
            and above.             
        </p>   
            <br />
        <p>
            This control can display four types of BarCharts: Column, StackedColumn, Bar and StackedBar.
        </p>
            <br />        
    </asp:Panel>
    <asp:Panel ID="Properties_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <asp:ImageButton ID="Properties_ToggleImage" runat="server" ImageUrl="~/images/expand.jpg"
                AlternateText="expand" />
            BarChart Properties
        </div>
    </asp:Panel>
    <asp:Panel ID="Properties_ContentPanel" runat="server" Style="overflow: hidden;"
        Height="0px">
        <p>
            The control above is initialized with this code. The <em>italic</em> properties
            are optional:</p>
        <pre>
&lt;ajaxToolkit:BarChart ID="Chart1" runat="server" 
<em>ChartHeight</em>="300" <em>
ChartWidth</em>="450" <em>
ChartTitle</em>=&quot;Test Title&quot; 
CategoriesAxis=&quot;2007,2008,2009,2010,2011,2012&quot;
ChartType=&quot;Column&quot; <em>
TitleColor</em>="red" <em>
CategoryAxisLineColor</em>="red" <em>
ValueAxisLineColor</em>="blue" <em>
BaseLineColor</em>=&quot;green&quot; &gt;
&lt;Series&gt;
&lt;ajaxToolkit:BarChartSeries Name=&quot;World&quot; <em>BarColor</em>=&quot;pink&quot; &gt;
&lt;DataValues&gt;
    &lt;ajaxToolkit:DataValue Data=&quot;110&quot; /&gt;
    &lt;ajaxToolkit:DataValue Data=&quot;189&quot; /&gt;
    &lt;ajaxToolkit:DataValue Data=&quot;255&quot; /&gt;
    &lt;ajaxToolkit:DataValue Data=&quot;95&quot; /&gt;
    &lt;ajaxToolkit:DataValue Data=&quot;107&quot; /&gt;
    &lt;ajaxToolkit:DataValue Data=&quot;140&quot; /&gt;
&lt;/DataValues&gt;
&lt;/ajaxToolkit:BarChartSeries&gt;
&lt;ajaxToolkit:BarChartSeries Name=&quot;United States&quot;  <em>BarColor</em>="yellow" &gt;
&lt;DataValues&gt;
    &lt;ajaxToolkit:DataValue Data="49" /&gt;
    &lt;ajaxToolkit:DataValue Data="77" /&gt;
    &lt;ajaxToolkit:DataValue Data="95" /&gt;
    &lt;ajaxToolkit:DataValue Data="68" /&gt;
    &lt;ajaxToolkit:DataValue Data="70" /&gt;
    &lt;ajaxToolkit:DataValue Data="79" /&gt;
&lt;/DataValues&gt;
&lt;/ajaxToolkit:BarChartSeries&gt;
&lt;/Series&gt;
&lt;/ajaxToolkit:BarChart&gt;
    </pre>
        <br />
        <b>Properties</b>
        <ul>
            <li><strong>ChartHeight</strong> - This property enables you to customize the height of the chart.</li>
            <li><strong>ChartWidth</strong> - This property enables you to customize the width of the chart.</li>
            <li><strong>ChartTitle</strong> - This property enables you to set the title of the chart.</li>
            <li><strong>CategoryAxis</strong> - This is a required property. You need to provide a set of 
            values for the category axis to create a bar chart.</li>
            <li><strong>ChartType</strong> - This property enables you to render different types of bar charts 
            including Column, StackedColumn, Bar, and StackedBar.</li>
            <li><strong>Theme</strong> - This property enables you to control the appearance of the bar chart 
            with a Cascading Style Sheet file.</li>
            <li><strong>ValueAxisLines</strong> - This property enables you to set the interval size for the 
            value axis line.</li>
            <li><strong>TitleColor</strong> - This property enables you to set the font color of the 
            chart title.</li>
            <li><strong>CategoryAxisLineColor</strong> - This property enables you to set the color of the 
            category axis lines.</li>
            <li><strong>ValueAxisLineColor</strong> - This property enables you to set the the color of the 
            value axis lines.</li>                
            <li><strong>BaseLineColor</strong> - This property enables you to set the color of the base lines
            of the chart.</li>
            BarChart Series Properties:
                <li><strong>Name</strong> - This property is required.</li>
            <li><strong>BarColor</strong> - This property enables you to set the color of bar for a particular 
            series.</li>            
        </ul>
        <br />        
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeDescription" runat="Server" TargetControlID="Description_ContentPanel"
        ExpandControlID="Description_HeaderPanel" CollapseControlID="Description_HeaderPanel"
        Collapsed="False" ImageControlID="Description_ToggleImage" />
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeProperties" runat="Server" TargetControlID="Properties_ContentPanel"
        ExpandControlID="Properties_HeaderPanel" CollapseControlID="Properties_HeaderPanel"
        Collapsed="True" ImageControlID="Properties_ToggleImage" />
</asp:Content>