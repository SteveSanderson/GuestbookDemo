<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Guestbook.Domain.Entities.GuestbookEntry>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Guestbook
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Guestbook</h2>

    Here are the current entries:

    <ul id="entries">
        <% foreach (var entry in Model) { %>
            <li class="entry">                
                <span class="author"><%= Html.Encode(entry.Author) %></span>
                posted at
                <span class="postedDate"><%= Html.Encode(entry.PostedDate) %></span>
                <div class="comment"><%= Html.Encode(entry.Comment) %></div>
            </li>
        <% } %>
    </ul>

    <button onclick="location.href = '<%= Url.Action("Post") %>'">
        Post a New Entry
    </button>
</asp:Content>
