using System;
using RestSharp;

using KasperskySnake.Api.Models;
using KasperskySnake.Api.Exceptions;

using Newtonsoft.Json;

namespace KasperskySnake.Api
{
	public class KasperskySnakeApi
	{
		RestClient client;
		public KasperskySnakeApi()
		{
			client = new RestClient("http://safeboard.northeurope.cloudapp.azure.com");

		}

		public void ThrowIfNonSuccessResponseCodeReceived(IRestResponse response)
		{
			if (!response.IsSuccessful)
			{
				throw new KasperskySnakeApiException("Сервер не вернул 200 :(", response);
			}
		}

		public GameStateResponse GetGameboard()
		{
			var request = new RestRequest("/api/Player/gameboard", Method.GET);
			request.RequestFormat = DataFormat.Json;
			IRestResponse<GameStateResponse> response = client.Execute<GameStateResponse>(request);
			ThrowIfNonSuccessResponseCodeReceived(response);
			return JsonConvert.DeserializeObject< GameStateResponse>( response.Content);
		}

		public NameResponse GetPlayerNameByToken(string token)
		{
			var request = new RestRequest("/api/Player/name", Method.GET);
			request.RequestFormat = DataFormat.Json;
			request.AddParameter("token", token);
			IRestResponse<NameResponse> response = client.Execute<NameResponse>(request);
			ThrowIfNonSuccessResponseCodeReceived(response);
			return JsonConvert.DeserializeObject<NameResponse>(response.Content);
		}

		public void PlayerSetDirection(DirectionRequest directionRequest)
		{
			var request = new RestRequest("/api/Player/direction", Method.POST);
			request.RequestFormat = DataFormat.Json;
			request.AddJsonBody(directionRequest);
			IRestResponse response = client.Execute(request);
			ThrowIfNonSuccessResponseCodeReceived(response);
			//return JsonConvert.DeserializeObject<NameResponse>(response.Content);
		}

	}
}
